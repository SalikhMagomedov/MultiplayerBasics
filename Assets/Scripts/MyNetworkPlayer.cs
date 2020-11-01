using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string displayName = "Missing name";
    [SyncVar] [SerializeField] private Color displayColor = Color.black;


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
}