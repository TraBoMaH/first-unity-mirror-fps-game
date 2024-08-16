using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class SniperRifleShooting : NetworkBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioclip;
    public float firerate = 30f;
    public ParticleSystem muzzle;
    private float nextfire = 0f;
    public GameObject hiteffect;
    public int MaxMag = 10;
    public int MaxReserveMag;
    private int NowMaxMag;
    public Camera playerCamera; // Ссылка на камеру
    public float hitRange = 100f; // Дальность хитскана
    public TextMeshProUGUI NowAmmoMag;
    public TextMeshProUGUI NowReserveAmmo;
    public Recoil recoilObj;
    public Animator animatorIK;
    private Animator animator2;
    public GameObject hitMarker;
    private void Start()
    {
        NowMaxMag = MaxMag; ShowAmmo();
        animator2 = GetComponent<Animator>();
    }
    private void Update()
    {
        if(!isLocalPlayer) return;
        if (Input.GetKeyUp(KeyCode.Mouse0) && Time.time > nextfire)
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
        NowMaxMag--;recoilObj.Fire(); ShowAmmo();
        animatorIK.Play("FireSniper"); animator2.Play("Fire");

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, hitRange))
        {
            GameObject impact = Instantiate(hiteffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);

            
            if (hit.collider.CompareTag("Enemy"))
            {
                health enemyHealth = hit.collider.GetComponent<health>();
                if (enemyHealth != null)
                {
                    enemyHealth.CMDtakedmg(100);
                    StartCoroutine(Hitmarker());
                }
                
            }
        }
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
        ShowAmmo(); // может быть понадобиться !islocalplayrt return;
    }
    [Command]
    private void CMDShoot()
    {
        RPCShoot();
    }
    private IEnumerator Hitmarker()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitMarker.SetActive(false);
    }
}