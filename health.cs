using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Mirror.Examples.Common;

public class health : NetworkBehaviour 
{
public float Maxhp = 100;
public float righthp;
public Image hpbar;
public P90Shooting P90;
public SniperRifleShooting sniper;
public RevolverShooting revolver;
public RocketLauncherShooting rocketLauncher;
public ShotGunShoot shotGun;
private GameObject SpawnPoints;
private GameObject mySelf;
    void Start()
    {
        
        if(!isLocalPlayer) return;
        righthp = Maxhp;
        SpawnPoints = GameObject.Find("Spawn1");
        mySelf = gameObject;
    }
    public void CMDtakedmg(float dmg)
    {
        if(!isLocalPlayer) return; // проблема может быть в этом
        righthp -= dmg;
        hpbar.fillAmount = righthp / 100f;
        if(righthp <= 0)
        {
            if(isServer)
            {
                RPCSetState(false);
                Invoke("RPCRespawn", 0.1f);
            }
            else
            {
                CMDSetState(false);
                Invoke("CMDRespawn", 0.1f);
            }
        } 
    }
    [ClientRpc]
    private void RPCSetState(bool a)
    {
        gameObject.SetActive(a);
    }
    [Command]
    private void CMDSetState(bool a)
    {
        RPCSetState(a);
    }
    private void RPCRespawn()
    {
        if(!isLocalPlayer) return;
        P90.PickUpBullets(100);
        sniper.PickUpBullets(10);
        revolver.PickUpBullets(30);
        rocketLauncher.PickUpBullets(30);
        shotGun.PickUpBullets(30);
        mySelf.transform.position = SpawnPoints.transform.position;
        righthp = Maxhp;
        RPCSetState(true);
        hpbar.fillAmount = righthp / 100f;
    }
    private void CMDRespawn()
    {
        if(!isLocalPlayer) return;
        P90.PickUpBullets(100);
        sniper.PickUpBullets(10);
        revolver.PickUpBullets(30);
        rocketLauncher.PickUpBullets(30);
        shotGun.PickUpBullets(30);
        mySelf.transform.position = SpawnPoints.transform.position;
        righthp = Maxhp;
        CMDSetState(true);
        hpbar.fillAmount = righthp / 100f;
    }
    public void Heal(float amount)
    {
        if(!isLocalPlayer) return;
        righthp = amount;
        hpbar.fillAmount = righthp / 100f;
    }
}
  