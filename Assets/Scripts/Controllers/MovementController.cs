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
                {
                    MoveableClickable move = SelectionController.Selected[0].GetComponent<MoveableClickable>();
                    if (move)
                        move.ExecuteMovementOrder(ordr);
                }
                break;
            case SelectionType.SingleGroup:
                {
                    List<ClickableDeployment> moveobject = SelectionController.SelectedDeployable.ClickableDeployments;
                    for (int i = 0; i < moveobject.Count; i++)
                    {
                        MoveableClickable moveable = moveobject[i].GetComponent<MoveableClickable>();
                        if (moveable)
                        {
                            Vector3 offset = new Vector3((i * moveobject[i].SpawnRadius) - ((moveobject.Count - 1) * moveobject[i].SpawnRadius) / 2.0f, 0, 0);
                            moveable.ExecuteMovementOrder(ordr + offset);
                        }
                    }
                }
                break;
            case SelectionType.None:
                break;
            case SelectionType.MultipleClickables:
                for (int i = 0; i < SelectionController.Selected.Count; i++)
                {
                    MoveableClickable moveable = SelectionController.Selected[i].GetComponent<MoveableClickable>();
                    if (moveable)
                    {
                        Vector3 offset = new Vector3((i * SelectionController.Selected[i].SpawnRadius) - ((SelectionController.Selected.Count - 1) * SelectionController.Selected[i].SpawnRadius) / 2.0f, 0, 0);
                        moveable.ExecuteMovementOrder(ordr + offset);
                    }
                }
                break;
        }

    }



}
