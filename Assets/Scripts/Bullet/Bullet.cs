using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public BulletData Data;

    public abstract void Init(GameObject Target, Weapon Weapon);
}
