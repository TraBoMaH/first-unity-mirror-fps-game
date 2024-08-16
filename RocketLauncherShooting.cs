using Mirror;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System.Runtime.Serialization;

public class RocketLauncherShooting : NetworkBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioclip;
    public float firerate = 30f;
    public ParticleSystem muzzle;
    private float nextfire = 0f;
    public int MaxMag = 10;
    private int NowMaxMag;
    public TextMeshProUGUI NowAmmoMag;
    public Recoil recoilObj;
    public Animator animatorIK;
    private Animator animator2;
    public GameObject Rocket;
    public Transform spawnPoint;
    public int speed = 10;
    private void Start()
    {   
        NowMaxMag = MaxMag;
        animator2 = GetComponent<Animator>();
        ShowAmmo();
    }
    private void Update()
    {
        if(!isLocalPlayer) return;
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextfire)
        {
            if(isServer)
            {
                nextfire = Time.time + 1f / firerate;
                RPCShoot();
            }
            else
            {
                nextfire = Time.time + 1f / firerate;
                CMDShoot();
            }
        }    
    }
    [ClientRpc]
    private void RPCShoot()
    {
        if(NowMaxMag > 0)
        {
        audiosource.PlayOneShot(audioclip);
        muzzle.Play(); 
        NowMaxMag--;recoilObj.Fire();ShowAmmo();
        animatorIK.Play("RocketFIre"); animator2.Play("Fire");
        GameObject obj = Instantiate(Rocket, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rig = obj.GetComponent<Rigidbody>();
        Vector3 tgr = obj.transform.forward;
        rig.velocity = tgr * speed * -1;
        obj.transform.Rotate(0f, -90f, 0f);
        }
    }
    /*private IEnumerator Reload()
    {
        reloadState = true;
        yield return new WaitForSeconds(2.5f);
        int reason = MaxMag - NowMaxMag;
        int bulletsToReload = Mathf.Min(reason, NowReserveMag);  // может удалить надо будет
        NowReserveMag -= bulletsToReload;
        NowMaxMag += bulletsToReload;
        reloadState = false;
    }*/
    private void ShowAmmo()
    {
        NowAmmoMag.text = NowMaxMag.ToString();
        //NowReserveAmmo.text = NowReserveMag.ToString();
    }
    public void PickUpBullets(int bullets)
    {
        NowMaxMag = Mathf.Clamp(bullets, 0, MaxMag);
        ShowAmmo();
    }
    [Command]
    private void CMDShoot()
    {
        RPCShoot();
    }
}
