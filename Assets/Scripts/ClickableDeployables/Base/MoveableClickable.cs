using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableClickable : ClickableDeployment
{
    [SerializeField]
    private NavMeshAgent Agent;

    private ClickableDeployment AttackTarget;
    private List<Vector3> MovmentOders;

    public void OrderMovements(List<Vector3> movments)
    {
        MovmentOders = movments;
    }
    public void AddMovementOrder(Vector3 transform)
    {
        MovmentOders.Add(transform);
    }

    private void ExecuteMovementOrders()
    {
        foreach (Vector3 order in MovmentOders)
        {
            ExecuteMovementOrder(order);
            if (!pathComplete())
                return;
        }
    }


    public virtual void Start()
    {
        LocalName = "Moveable" + Random.Range(0, 100);
        Agent.updateUpAxis = true;
        Agent.updateRotation = true;
        MovmentOders = new List<Vector3>();
    }

    public virtual void Update()
    {   //temp redo before commit
       // ExecuteMovementOrders();
    }

    public void ExecuteMovementOrder(Vector3 order)
    {
        Agent.SetDestination(order);
        transform.LookAt(order);
    }

    private bool pathComplete()
    {
        if (Vector3.Distance(Agent.destination, Agent.transform.position) <= Agent.stoppingDistance)
        {
            if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }

    public override void TriggerOnClick()
    {
        if (ClickObject)
            ClickObject.SetActive(true);
    }

    public override void TriggerOnAddedToSelection()
    {
        if (ClickObject)
            ClickObject.SetActive(true);
    }

    public override void TriggerDeselect()
    {
        if (ClickObject)
            ClickObject.SetActive(false);
    }

    public override void TriggerOnHover()
    {
        if (HoverObject)
            HoverObject.SetActive(true);
    }

    public override void TriggerOffHover()
    {
        if (HoverObject)
            HoverObject.SetActive(false);
    }

    public override void Init(Deployable deployable)
    {
        DeployableRef = deployable;
    }
}
