using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Mathematics;

public class RocketBeh : NetworkBehaviour
{
public GameObject particle;
public AudioClip audiotoplay;
public Rigidbody rig;
private void OnTriggerEnter(Collider other) 
{
    if(other.CompareTag("Untagged") || other.CompareTag("Enemy"))
    {
    Collider[] targets = Physics.OverlapSphere(gameObject.transform.position, 5f, 1 << 10);
    GameObject spawn = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
    NetworkServer.Spawn(spawn); AudioSource source = spawn.GetComponent<AudioSource>();
    source.PlayOneShot(audiotoplay);
    
    foreach (Collider target in targets)
    {
        health hp = target.GetComponent<health>();
        hp.CMDtakedmg(40);
    }
    Destroy(gameObject); NetworkServer.Destroy(gameObject);
    //Destroy(spawn, 3f); DestroySpawn(spawn, 3f);
    }
    
}
private IEnumerator DestroySpawn(GameObject a, float b)
{
    yield return new WaitForSeconds(b);
    NetworkServer.Destroy(a);
}
}
