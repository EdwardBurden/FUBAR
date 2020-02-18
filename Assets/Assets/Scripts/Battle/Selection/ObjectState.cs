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

 

    public override void BeginPreview(PreviewOrder order)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewController.Instance.BeginPreview(order, objects);
    }

    public override void EndPreview()
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
            int Columns = Formations.GetColumnsFromDistance(ordr.Start, ordr.End);
            posList = Formations.OrganiseSquareFormationFromCorner(ordr.Start, ordr.End, objects, Columns, Constants.K_DefaultSpace); //remove numbers later
        }
        else
            posList = Formations.OrganiseSquareFormation(ordr.Destination, objects, Constants.K_DefaultColumns, Constants.K_DefaultSpace);

        for (int i = 0; i < objects.Count; i++)
        {
            AttachComponent comp = objects[i].GetComponent<AttachComponent>();
            if (comp)
                comp.Dettach();

            MovementComponent agent = objects[i].GetComponent<MovementComponent>();
            if (agent)
                agent.Move(posList[i]);
        }
    }

    public override void StateLost()
    {
        //throw new System.NotImplementedException();
        //PreviewManager.close
    }
}
