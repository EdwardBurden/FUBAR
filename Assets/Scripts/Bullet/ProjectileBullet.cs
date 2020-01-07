using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Bullet
{
    private float TimeCount = 0;
    private Vector3 Forward;
    private int damage = 50;
    public override void Init(GameObject enemy, Weapon Weapon)
    {
        Forward = enemy.transform.position - this.transform.position;
    }

    private void Update()
    {
        this.transform.position += Forward * Time.deltaTime * Data.BulletSpeed;
        TimeCount += Time.deltaTime;
        if (TimeCount > 1)
            Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject && collision.gameObject.GetComponent<ClickableDeployment>())
        {
            collision.gameObject.GetComponent<ClickableDeployment>().Health -= 50;
            Destroy(this.gameObject);
        }

    }
}
