using UnityEngine;
using Mirror; 
using TMPro;
using System.Collections;

public class ShotGunShoot : NetworkBehaviour
{
public AudioSource source;
public AudioClip clip;
public Recoil recoil;
public float firerate = 30;
public float nextfire = 0;
public ParticleSystem particle;
public int MaxMag = 10;
private int NowMaxMag;
public GameObject shootPoint;
public float hitRange = 100;
public TextMeshProUGUI NowMaxMagTMP;
public Animator animatorIK;
public Animator shotgunAnim;
public GameObject hitEffect;
private int bulletCount = 7;
public float spreadAngle;
public GameObject hitMarker;
private void Start() 
{
    NowMaxMag = MaxMag;
    ShowAmmo();
}
private void Update() 
{
    if(!isLocalPlayer) return;
    if(Input.GetKeyDown(KeyCode.Mouse0))
    {
        if( Time.time > nextfire)
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
}
[ClientRpc]
private void RPCShoot()
{
    if(NowMaxMag > 0)
    {
        NowMaxMag--;
        for(int i = 0; i < bulletCount; i++)
        {
        
        particle.Play();
        recoil.Fire(); ShowAmmo();
        shotgunAnim.Play("Fire"); animatorIK.Play("ShotGunFire");
        
        /*Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;*/
        Vector3 direction = shootPoint.transform.forward / 1;
        direction.x += Random.Range(-spreadAngle, spreadAngle);
        direction.y += Random.Range(-spreadAngle, spreadAngle);

        if (Physics.Raycast(shootPoint.transform.position, direction, out RaycastHit hit))

        //if(Physics.Raycast(ray, out hit, hitRange))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);

            if(hit.collider.CompareTag("Enemy"))
            {
                health enemyhealth = hit.collider.GetComponent<health>();
                if(enemyhealth != null)
                {
                    enemyhealth.CMDtakedmg(5);
                    StartCoroutine(Hitmarker());
                }
                
            }
        }
        }
        source.PlayOneShot(clip);
    }
}
private void ShowAmmo()
{
    NowMaxMagTMP.text = NowMaxMag.ToString();
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
private IEnumerator Hitmarker()
{
    hitMarker.SetActive(true);
    yield return new WaitForSeconds(0.1f);
    hitMarker.SetActive(false);
}
}
