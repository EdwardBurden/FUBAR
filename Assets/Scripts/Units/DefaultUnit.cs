using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class DefaultUnit : ClickableDeployment
{
    private NavMeshAgent Agent;
    public Weapon MainWeapon;
    public int MeleeRange = 1;
    public bool enemy = false;
    public ClickableDeployment EnemyTarget;

    public override void Start()
    {
        base.Start();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateUpAxis = true;
        Agent.updateRotation = true;
        TargetUp = this.transform.up;
    }

    public Vector3 TargetUp;

    public override void Update()
    {
        base.Update();
        if (EnemyTarget != null)
        {
            transform.LookAt(EnemyTarget.transform, Vector3.up);
            MainWeapon.Fire(EnemyTarget.gameObject);
        }
    }

    //this will be replaced with climbing animation later
    public void OrientUnit()
    {
        if (!Agent.isStopped)
        {
            RaycastHit hit;
            Ray ray = new Ray(this.transform.position, -transform.up);
            if (Physics.Raycast(ray, out hit))
            {
                //float angle = Vector3.Angle(Vector3.down, hit.normal);
                TargetUp = hit.normal;
            }
            transform.up = Vector3.Lerp(transform.up, TargetUp, Time.deltaTime * 10);
            transform.rotation = Quaternion.LookRotation(Agent.velocity.normalized);
        }

    }

    public void MoveToTarget(Vector3 target)
    {
        GetComponent<NavMeshAgent>().SetDestination(target);
    }

    public override void EnemyDetected(ClickableDeployment enemy)
    {
        EnemyTarget = enemy;
    }

    public override void EnemyLost()
    {
        EnemyTarget = null;
    }


}
