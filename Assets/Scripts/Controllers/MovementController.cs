using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public class MovementInfo
    {
        public ClickableDeployment Object;
        public Transform TargetTransform;
    }


    public SelectionController SelectionController;
    private void Update()
    {
        if (IsMovementOrdered())
        {
            Vector3 order = GetOrder();
            SendOrder(order);
        }
    }

    private bool IsMovementOrdered()
    {
        return Input.GetMouseButtonUp(1) && Helpers.CheckIfObjectIsInFocus<TerrainClickable>().Focused;
    }

    public Vector3 GetOrder()
    {
        RayCastInfo<TerrainClickable> info = Helpers.CheckIfObjectIsInFocus<TerrainClickable>();
        return info.HitInfo.point;
    }

    public void SendOrder(Vector3 ordr)
    {
        switch (SelectionController.CurrentSelection)
        {
            case SelectionType.SingleClickable:
                SendOrder(SelectionController.Selected, ordr);
                break;
            case SelectionType.SingleGroup:
                SendOrder(SelectionController.SelectedDeployable.ClickableDeployments, ordr);
                break;
            case SelectionType.None:
                break;
            case SelectionType.MultipleClickables:
                SendOrder(SelectionController.Selected, ordr);
                break;
        }
    }

    private void SendOrder(List<ClickableDeployment> clickableDeployments, Vector3 target)
    {
        for (int i = 0; i < clickableDeployments.Count; i++)
        {
            MoveableClickable moveable = clickableDeployments[i].GetComponent<MoveableClickable>();
            if (moveable)
            {
                RayCastInfo<AttachmentComponent> atcomp = Helpers.CheckIfObjectIsInFocus<AttachmentComponent>();
                if (atcomp.Focused)
                {
                    atcomp.

                }
                else
                {
                    Vector3 offset = new Vector3((i * moveable.SpawnRadius) - ((clickableDeployments.Count - 1) * moveable.SpawnRadius) / 2.0f, 0, 0);
                    if (Input.GetKey(KeyCode.LeftControl))
                        moveable.AddMovementOrder(target + offset);
                    else
                        moveable.NewMovementOrder(target + offset);
                }
            }
        }
    }
}
