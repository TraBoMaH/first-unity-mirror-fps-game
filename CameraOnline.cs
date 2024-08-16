using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOnline : NetworkBehaviour
{
private Camera PlayerCamera;
private void Start() {
    
    if(!isLocalPlayer) return;

    PlayerCamera = gameObject.GetComponent<Camera>();
    PlayerCamera.enabled = true;
}
}
