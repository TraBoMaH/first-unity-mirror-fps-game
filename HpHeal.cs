using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HpHeal : NetworkBehaviour
{
public float HealAmount = 0;
public Transform stashPos;
private GameObject self;
private void Start() {
    self = gameObject;
}
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Enemy"))
    {
        health hp; hp = other.GetComponent<health>();
        hp.Heal(HealAmount);
        StartCoroutine(Stop());
    }
}
private IEnumerator Stop()
{
    
    Vector3 ttransform = self.transform.position;
    self.transform.position = stashPos.position;
    yield return new WaitForSeconds(10f);
    self.transform.position = ttransform;
}
}
