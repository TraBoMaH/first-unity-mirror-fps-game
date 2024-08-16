using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class WeaponSwitcher : NetworkBehaviour
{

public GameObject[] weapons;
public GameObject[] weaponsBulletsTMP;

private void Update() 
{
    if(!isLocalPlayer) return;
    if(Input.GetKeyDown(KeyCode.Alpha1))
    {
        if(isServer)
        {
            RPCSwitch0();
        }
        else{CMDSwitch0();}
    }
    if(Input.GetKeyDown(KeyCode.Alpha2))
    {
        if(isServer)
        {
            RPCSwitch1();
        }
        else{CMDSwitch1();}
    }
    if(Input.GetKeyDown(KeyCode.Alpha3))
    {
        if(isServer)
        {
            RPCSwitch2();
        }
        else{CMDSwitch2();}
    }
    if(Input.GetKeyDown(KeyCode.Alpha4))
    {
        if(isServer)
        {
            RPCSwitch3();
        }
        else{CMDSwitch3();}
    }
    if(Input.GetKeyDown(KeyCode.Alpha5))
    {
        if(isServer)
        {
            RPCSwitch4();
        }
        else{CMDSwitch4();}
    }
}
private void SwitchAnyOf(int weaponIndex)
{
    if (weaponIndex >= 0 && weaponIndex < weapons.Length)
    {
        // Деактивируем все оружия
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // Активируем выбранное оружие
        weapons[weaponIndex].SetActive(true); 
    }
}
private void SwitchAnyOfTMP(int TMPIndex)
{
        if (TMPIndex >= 0 && TMPIndex < weaponsBulletsTMP.Length)
    {
        // Деактивируем все TMPUGUI
        foreach (var TMP in weaponsBulletsTMP)
        {
            TMP.SetActive(false);
        }

        // Активируем выбранное оружие
        weaponsBulletsTMP[TMPIndex].SetActive(true); 
    }
}
[ClientRpc]
public void RPCSwitch0()
{
    SwitchAnyOf(0);
    SwitchAnyOfTMP(0);
}
[ClientRpc]
public void RPCSwitch1()
{
    SwitchAnyOf(1);
    SwitchAnyOfTMP(1);
}
[ClientRpc]
public void RPCSwitch2()
{
    SwitchAnyOf(2);
    SwitchAnyOfTMP(2);
}
[ClientRpc]
public void RPCSwitch3()
{
    SwitchAnyOf(3);
    SwitchAnyOfTMP(3);
}
[ClientRpc]
public void RPCSwitch4()
{
    SwitchAnyOf(4);
    SwitchAnyOfTMP(4);
}
[Command]
public void CMDSwitch0()
{
    SwitchAnyOf(0);
    SwitchAnyOfTMP(0);
    RPCSwitch0();
}
[Command]
public void CMDSwitch1()
{
    SwitchAnyOf(1);
    SwitchAnyOfTMP(1);
    RPCSwitch1();
}
[Command]
public void CMDSwitch2()
{
    SwitchAnyOf(2);
    SwitchAnyOfTMP(2);
    RPCSwitch2();
}
[Command]
public void CMDSwitch3()
{
    SwitchAnyOf(3);
    SwitchAnyOfTMP(3);
    RPCSwitch3();
}
[Command]
public void CMDSwitch4()
{
    SwitchAnyOf(4);
    SwitchAnyOfTMP(4);
    RPCSwitch4();
}
}