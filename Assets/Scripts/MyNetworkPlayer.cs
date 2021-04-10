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

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor(BaseColor, newColor);
    }

    private void HandleDisplayNameText(string oldText, string newText)
    {
        displayNameText.text = newText;
    }
}