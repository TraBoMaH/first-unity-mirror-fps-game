using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
public Rigidbody rb;
public GameObject gameObjectt;
private void FixedUpdate() 
{
    if(Input.GetKeyDown(KeyCode.LeftShift))
    {
        Dash();
    }
}
private void Dash()
{
    rb.AddExplosionForce(100, gameObjectt.transform.position * 100, 10, 1, ForceMode.Impulse);
}
}
