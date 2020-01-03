using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Bullet
{
    private float TimeCount = 0;
    private Vector3 Forward;
    public override void Init(GameObject enemy, Weapon Weapon)
    {
        Forward = enemy.transform.position - this.transform.position;
    }

    private void Update()
    {
        this.transform.position += Forward * Time.deltaTime;
        TimeCount += Time.deltaTime;
        if (TimeCount > 2)
            Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
