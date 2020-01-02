using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : ClickableDeployment
{
    private NavMeshAgent Agent;
    
    public override void Start()
    {
        base.Start();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateUpAxis = false;
        TargetUp = this.transform.up;
    }

    public Vector3 TargetUp;

    private void Update()
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
        }
        transform.up = Vector3.Lerp(transform.up, TargetUp, Time.deltaTime * 10);
    }

    public void MoveToTarget(Vector3 target)
    {

        GetComponent<NavMeshAgent>().SetDestination(target);


    }
}
