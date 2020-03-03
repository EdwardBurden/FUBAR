using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    public abstract void UseWeapon(FUBAR.ClickObject target);

    public abstract bool CanUseWeapon(FUBAR.ClickObject target);
}
