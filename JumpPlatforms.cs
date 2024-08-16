using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatforms : MonoBehaviour
{
public float strength;    
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Enemy"))
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 100 * strength);
    }
}
}
