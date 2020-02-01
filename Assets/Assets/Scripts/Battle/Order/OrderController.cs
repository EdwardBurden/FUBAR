using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class OrderController : MonoBehaviour
    {
        public OrderEvent MoveOrderEvent;
        public OrderEvent AttackOrderEvent;
        public OrderEvent AttachOrderEvent;
        public SelectionController SelectionController;

        private Vector3 DragStartPos;
        private Vector3 DragEndPos;

        private RayCastInfo<ClickObject> LastClickedObject;

        private void HandleOrder()
        {

            if (Input.GetMouseButtonDown(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    LastClickedObject = cickedObject;
                    DragStartPos = LastClickedObject.HitInfo.point;
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    DragEndPos = cickedObject.HitInfo.point;
                    SendOrder(DragStartPos, DragEndPos);
                }
            }

            if (Input.GetMouseButton(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    DragEndPos = cickedObject.HitInfo.point;
                    GeneratePreview(DragStartPos, DragEndPos);
                }
            }
        }

        private void GeneratePreview(Vector3 start, Vector3 end)
        {
            switch (LastClickedObject.FocusObject.ClickType)
            {
                case ClickObjectType.Moveable:
                    PreviewOrder order = new PreviewOrder(start, end);
                    SelectionController.PreviewMovementOrder(order);
                    break;
                case ClickObjectType.Destructable:
                    break;
                case ClickObjectType.Attachable:

                    break;
                default:
                    break;
            }

        }

        private void SendOrder(Vector3 start, Vector3 end)
        {
            switch (LastClickedObject.FocusObject.ClickType)
            {
                case ClickObjectType.Moveable:
                    MoveOrder order = new MoveOrder(LastClickedObject.HitInfo.point);
                    MoveOrderEvent.Raise(order);
                    break;
                case ClickObjectType.Destructable:
                    AttackOrder attack = new AttackOrder(LastClickedObject.HitInfo.point);
                    AttackOrderEvent.Raise(attack);
                    break;
                case ClickObjectType.Attachable:
                    AttachOrder att = new AttachOrder(LastClickedObject.FocusObject.gameObject);
                    AttachOrderEvent.Raise(att);
                    break;
                default:
                    break;
            }
        }


        private void Update()
        {
            HandleOrder();
        }
    }
}
