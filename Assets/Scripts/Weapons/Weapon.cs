using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private string WeaponName;
    public WeaponData WeaponData;

    public abstract void Fire(GameObject gameObject);
}
