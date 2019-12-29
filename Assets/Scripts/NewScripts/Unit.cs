using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : ClickableDeployment
{
    public void MoveToTarget(Vector3 target)
    {

        GetComponent<NavMeshAgent>().SetDestination(target);


    }
}
