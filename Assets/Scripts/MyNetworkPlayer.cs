using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text displayNameText;
    [SerializeField] private Renderer displayColorRenderer;

    [SyncVar(hook = nameof(HandleDisplayNameText))] [SerializeField] private string displayName = "Missing name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color displayColor = Color.black;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    #region Server

    [Server]
    public void SetDisplayName(string value)
    {
        displayName = value;
    }

    [Server]
    public void SetDisplayColor(Color value)
    {
        displayColor = value;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogNewName(newDisplayName);
        
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor(BaseColor, newColor);
    }

    private void HandleDisplayNameText(string oldText, string newText)
    {
        displayNameText.text = newText;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}