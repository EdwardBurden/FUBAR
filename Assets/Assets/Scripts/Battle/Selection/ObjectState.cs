using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

public class ObjectState : SelectionState
{
    private ObjectSelectionManager ObjectSelection;
    private PreviewManager PreviewManager; //move out make static(maybe)

    public ObjectState(ObjectSelectionManager objectSelection, Transform previewtransform)
    {
        ObjectSelection = objectSelection;
        PreviewManager = new PreviewManager(previewtransform);
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

    public override void GeneratePreview(PreviewOrder previewOrder)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewManager.GeneratePreview(previewOrder, objects);
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
        PreviewManager.Clean();
    }

    public override void StateLost()
    {
        //throw new System.NotImplementedException();
        //PreviewManager.close
    }
}
