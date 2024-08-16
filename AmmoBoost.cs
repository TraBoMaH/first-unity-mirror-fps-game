using Mirror;
using UnityEngine;
using System.Collections;

public class AmmoBoost : NetworkBehaviour
{
public Transform stashPos;
private GameObject self;
private void Start() {
    self = gameObject;
}
private void OnTriggerEnter(Collider other) 
{
    if (other.CompareTag("Enemy"))
    {
        ShotGunShoot shotGun;
        SniperRifleShooting sniper;
        RocketLauncherShooting rocketLauncher;
        P90Shooting p90;
        RevolverShooting revolver;
        shotGun = other.GetComponentInChildren<ShotGunShoot>();
        sniper = other.GetComponentInChildren<SniperRifleShooting>();
        rocketLauncher = other.GetComponentInChildren<RocketLauncherShooting>();
        p90 = other.GetComponentInChildren<P90Shooting>();
        revolver = other.GetComponentInChildren<RevolverShooting>();
        if(shotGun != null)
        {
            shotGun.PickUpBullets(100);
        }
        if(sniper != null)
        {
            sniper.PickUpBullets(100);
        }
        if(rocketLauncher != null)
        {
            rocketLauncher.PickUpBullets(100);
        }
        if(p90 != null)
        {
            p90.PickUpBullets(100);
        }
        if(revolver != null)
        {
            revolver.PickUpBullets(100);
        }
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
