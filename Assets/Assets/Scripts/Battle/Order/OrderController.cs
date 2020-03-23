using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helpers;

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
        public float DragTimer = 0.1f;
        public float MinSpaceChange = 0.5f;
        private float Timer;
        private bool TimeDrag = false;

        private void HandleOrder()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    LastClickedObject = cickedObject;
                    DragStartPos = LastClickedObject.HitInfo.point;
                    DragEndPos = DragStartPos;
                    TimeDrag = true;
                    BeginPreview(DragStartPos, DragEndPos);
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    DragEndPos = cickedObject.HitInfo.point;
                    EndPreview();
                    SendOrder(DragStartPos, DragEndPos);
                    TimeDrag = false;
                    Timer = 0.0f;
                }
            }

            if (Input.GetMouseButton(1))
            {
                RayCastInfo<ClickObject> cickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
                if (cickedObject.Focused)
                {
                    DragEndPos = cickedObject.HitInfo.point;
                    if (Timer > DragTimer && MinSpaceChange < Vector3.Distance(DragStartPos, DragEndPos))
                        Preview(DragStartPos, DragEndPos);
                }
            }

            if (TimeDrag)
                Timer += Time.deltaTime;
        }

        private void Preview(Vector3 start, Vector3 end)
        {

            PreviewOrder order = new PreviewOrder(start, end, (Timer > DragTimer && MinSpaceChange < Vector3.Distance(start, end)));
            SelectionController.PreviewMovementOrder(order);



        }

        private void EndPreview()
        {
            SelectionController.EndPreviewOrder();

        }

        private void BeginPreview(Vector3 start, Vector3 end)
        {

            PreviewOrder order = new PreviewOrder(start, end, false);
            SelectionController.BeginPreviewOrder(order);


        }

        private void SendOrder(Vector3 start, Vector3 end)
        {
            MoveOrder order = new MoveOrder(LastClickedObject.HitInfo.point, start, end, (Timer > DragTimer && MinSpaceChange < Vector3.Distance(start, end)));
            MoveOrderEvent.Raise(order);

        }


        private void Update()
        {
            HandleOrder();
        }
    }
}
