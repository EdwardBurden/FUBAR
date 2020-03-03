using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;

public class RifleWeaponComponent : WeaponComponent
{
    public GameObject BulletPrefab;

    public float ReloadTime;
    public float FireRateTime;
    private float FireRateTimerStart;


    public override bool CanUseWeapon(ClickObject target)
    {
        return (Time.time - FireRateTimerStart) > FireRateTime;
    }

    public override void UseWeapon(ClickObject target)
    {
        FireRateTimerStart = Time.time;
    }
}
