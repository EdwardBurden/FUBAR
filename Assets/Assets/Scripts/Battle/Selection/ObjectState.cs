using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

public class ObjectState : SelectionState
{
    private ObjectSelectionManager ObjectSelection;

    public ObjectState(ObjectSelectionManager objectSelection)
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

    public override void BeginPreview(PreviewOrder order)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewController.Instance.BeginPreview(order, objects);
    }

    public override void EndPreview(PreviewOrder order)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewController.Instance.EndPreview();
    }

    public override void GeneratePreview(PreviewOrder previewOrder)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewController.Instance.Preview(previewOrder, objects);
    }

    public override void Init()
    {

    }

    public override void Move(MoveOrder ordr)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        List<Vector3> posList = new List<Vector3>();
        if (ordr.DragOrder)
        {
            float endDiffference = Vector3.Distance(ordr.End, ordr.Start);
            int Columns = ((int)endDiffference / 5) + 1;//5 = space
            posList = Formations.OrganiseSquareFormationFromCorner(ordr.Start, ordr.End, objects, Columns, 5); //remove numbers later
        }
        else
            posList = Formations.OrganiseSquareFormation(ordr.Destination, objects, 4, 5);

        for (int i = 0; i < objects.Count; i++)
        {
            AttachComponent comp = objects[i].GetComponent<AttachComponent>();
            if (comp)
                comp.Dettach();

            NavMeshAgent agent = objects[i].GetComponent<NavMeshAgent>();
            if (agent)
                agent.SetDestination(posList[i]);
        }
    }

    public override void StateLost()
    {
        //throw new System.NotImplementedException();
        //PreviewManager.close
    }
}
