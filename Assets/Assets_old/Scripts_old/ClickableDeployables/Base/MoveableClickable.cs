using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MoveableClickable : ClickableDeployment
{
    [SerializeField]
    private NavMeshAgent Agent;

    private ClickableDeployment AttackTarget;
    private List<Vector3> MovmentOders;

    public virtual void Start()
    {
        LocalName = "Moveable" + Random.Range(0, 100);
        Agent.updateUpAxis = true;
        Agent.updateRotation = true;
        MovmentOders = new List<Vector3>();
        Agent.ResetPath();
    }

    public virtual void Update()
    {
        if (pathComplete())
        {
            if (MovmentOders.Count > 0)
                ExecuteMovementOrder(MovmentOders[0]);
        }
    }


    public void NewMovementOrder(Vector3 orders)
    {
        MovmentOders.Clear();
        MovmentOders.Add(orders);
        ExecuteMovementOrder(orders);
    }

    public void AddMovementOrder(Vector3 orders)
    {
        MovmentOders.Add(orders);
    }


    private void ExecuteMovementOrder(Vector3 order)
    {
        Agent.SetDestination(order);
        transform.LookAt(order);
        MovmentOders.RemoveAt(0);
    }

    private bool pathComplete()
    {
        if (Helpers.FlatDistanceTo(Agent.destination, Agent.transform.position) <= Agent.stoppingDistance)
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

    public override void Init(Group deployable)
    {
        DeployableRef = deployable;
    }
}
