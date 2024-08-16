using UnityEngine;
using Mirror;

public class LeaveGamebehavior : NetworkBehaviour
{
    public void leave()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private void FixedUpdate() 
    {
        if(!isLocalPlayer) return;
        if(Input.GetKey(KeyCode.F12))
        {
            leave();
        }
    }
}