using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            List<Group> groups = SelectionManager.GetGroup();
            for (int i = 0; i < groups.Count; i++)
            {
                List<Vector3> posList = groups[i].GetMovementPreviewPosition(order, i);
                PreviewController.Instance.BeginPreview(posList, order, groups[i].GetObjects().Where(x => x.GetComponent<MovementComponent>()).ToList());
            }
        }

        public override void EndPreview()
        {
            PreviewController.Instance.EndPreview();
        }

        public override void GeneratePreview(PreviewOrder previewOrder)
        {
            List<Group> groups = SelectionManager.GetGroup();
            for (int i = 0; i < groups.Count; i++)
            {
                List<Vector3> posList = groups[i].GetMovementPreviewPosition(previewOrder, i);
                PreviewController.Instance.Preview(posList, previewOrder, groups[i].GetObjects().Where(x => x.GetComponent<MovementComponent>()).ToList());
            }
        }

        public override void Init()
        {
            // throw new System.NotImplementedException();
        }

        public override void Move(MoveOrder ordr)
        {
            List<Group> groups = SelectionManager.GetGroup();
            for (int i = 0; i < groups.Count; i++)
            {
                List<Vector3> posList = groups[i].GetMovementPosition(ordr, i);
                groups[i].Move(posList);
            }
        }

        public override void StateLost()
        {
            SelectionManager.ResetSelection();
        }

    }
}