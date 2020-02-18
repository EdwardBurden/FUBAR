using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{

    public class GroupState : SelectionState
    {
        private GroupSelectionManager SelectionManager;

        public GroupState(GroupSelectionManager selectionManager)
        {
            SelectionManager = selectionManager;
        }

        public override void BeginPreview(PreviewOrder order)
        {
            List<ClickObject> AllObjects = SelectionManager.GetAllObjects();
            PreviewController.Instance.BeginPreview(order, AllObjects);
        }

        public override void EndPreview( )
        {
            PreviewController.Instance.EndPreview();
        }

        public override void GeneratePreview(PreviewOrder previewOrder)
        {
            List<ClickObject> AllObjects = SelectionManager.GetAllObjects();
            PreviewController.Instance.Preview(previewOrder, AllObjects);
        }

        public override void Init()
        {
            // throw new System.NotImplementedException();
        }

        public override void Move(MoveOrder ordr)
        {
            List<ClickObject> AllObjects = SelectionManager.GetAllObjects();
            List<Vector3> posList = new List<Vector3>();
            if (ordr.DragOrder)
            {
                int Columns = Formations.GetColumnsFromDistance(ordr.Start, ordr.End);
                posList = Formations.OrganiseSquareFormationFromCorner(ordr.Start, ordr.End, AllObjects, Columns, Constants.K_DefaultSpace); //remove numbers later
            }
            else
                posList = Formations.OrganiseSquareFormation(ordr.Destination, AllObjects, Constants.K_DefaultColumns, Constants.K_DefaultSpace);

            for (int i = 0; i < AllObjects.Count; i++)
            {
                AttachComponent comp = AllObjects[i].GetComponent<AttachComponent>();
                if (comp)
                    comp.Dettach();

                MovementComponent agent = AllObjects[i].GetComponent<MovementComponent>();
                if (agent)
                    agent.Move(posList[i]);
            }
        }

        public override void StateLost()
        {
            SelectionManager.ResetSelection();
        }
    }
}