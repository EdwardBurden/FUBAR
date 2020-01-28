using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

public class ObjectState : SelectionState
{
    private FUBAR.SelectionController selectionController;
    private ObjectSelectionManager ObjectSelection;

    public ObjectState(FUBAR.SelectionController selectionController, ObjectSelectionManager objectSelection)
    {
        this.selectionController = selectionController;
        ObjectSelection = objectSelection;
    }

    public override void Attach()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Dettach()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        //throw new System.NotImplementedException();
    }

    public override void Move(MoveOrder ordr)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        foreach (ClickObject item in objects)
        {
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
