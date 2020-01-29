using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

public class ObjectState : SelectionState
{
    private ObjectSelectionManager ObjectSelection;

    public ObjectState( ObjectSelectionManager objectSelection)
    {
        ObjectSelection = objectSelection;
    }

    public override void Attach(AttachOrder attachOrder)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        foreach (ClickObject item in objects)
        {
            AttachComponent comp = item.GetComponent<AttachComponent>();
            if (comp)
                comp.Attach(attachOrder.Anchor.transform);
        }
    }

    public override void Attack(AttackOrder order)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        foreach (ClickObject item in objects)
        {
            AttachComponent comp = item.GetComponent<AttachComponent>();
            if (comp)
                comp.Dettach();
            NavMeshAgent agent = item.GetComponent<NavMeshAgent>();
            if (agent)
                agent.SetDestination(order.GetShootingDistance(agent.transform.position));
        }
    }

    public override void Init()
    {
    
    }

    public override void Move(MoveOrder ordr)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        foreach (ClickObject item in objects)
        {
            AttachComponent comp = item.GetComponent<AttachComponent>();
            if (comp)
                comp.Dettach();

            NavMeshAgent agent = item.GetComponent<NavMeshAgent>();
            if (agent)
                agent.SetDestination(ordr.Destination);
        }
    }

    public override void StateLost()
    {
        //throw new System.NotImplementedException();
    }
}
