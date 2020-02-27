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
        List<Vector3> poss;
        SquareFormation formation = new SquareFormation();
        if (!order.Drag)
        {
            poss = formation.GetFormationPosition(order.start, objects, Constants.K_DefaultColumns, Constants.K_DefaultSpace);
        }
        else
        {
            poss = formation.GetDragFormationPosition(order.start, order.end, objects, Constants.K_DefaultColumns, Constants.K_DefaultSpace);
        }
        PreviewController.Instance.BeginPreview(poss, order, objects);
    }

    public override void EndPreview()
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        PreviewController.Instance.EndPreview();
    }

    public override void GeneratePreview(PreviewOrder previewOrder)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        SquareFormation formation = new SquareFormation();
        int columns = formation.GetColumnsFromDistance(previewOrder.start, previewOrder.end, Constants.K_DefaultSpace);
        List<Vector3> poss = formation.GetDragFormationPosition(previewOrder.start, previewOrder.end, objects, columns, Constants.K_DefaultSpace);
        PreviewController.Instance.Preview(poss, previewOrder, objects);
    }

    public override void Init()
    {

    }

    public override void Move(MoveOrder ordr)
    {
        List<ClickObject> objects = ObjectSelection.GetSelectedObjects();
        List<Vector3> posList = new List<Vector3>();
        SquareFormation formation = new SquareFormation();
        if (ordr.DragOrder)
        {
            int Columns = formation.GetColumnsFromDistance(ordr.Start, ordr.End, Constants.K_DefaultSpace);
            posList = formation.GetDragFormationPosition(ordr.Start, ordr.End, objects, Columns, Constants.K_DefaultSpace); //remove numbers later
        }
        else
            posList = formation.GetFormationPosition(ordr.Destination, objects, Constants.K_DefaultColumns, Constants.K_DefaultSpace);

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
