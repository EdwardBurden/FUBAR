using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : Weapon
{
    public override void Fire(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
