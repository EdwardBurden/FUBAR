using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private string WeaponName;
    public WeaponData WeaponData;

    private void Start()
    {
       // WeaponName = WeaponData.WeaponName;
    }

    public void UseWeaponRanged(GameObject target)
    {
        Debug.Log("shoot");
    }

    public void UseWeaponMelee(GameObject target)
    {

    }


}
