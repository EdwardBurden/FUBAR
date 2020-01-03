using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public float timeocunter = 0;
    public Bullet Projectile;
    public Transform SpawnPoint;
    public override void Fire(GameObject gameObject)
    {
        timeocunter += Time.deltaTime;
        if (timeocunter > 0.3)
        {
            Bullet bullet = Instantiate(Projectile, SpawnPoint.position, SpawnPoint.rotation);
            bullet.Init(gameObject, this);
            timeocunter = 0;
        }
    }
}
