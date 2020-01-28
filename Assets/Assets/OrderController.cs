using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public abstract class Order
    {


    }
    public class MoveOrder : Order
    {
        public Vector3 Destination;

        public MoveOrder(Vector3 destination)
        {
            Destination = destination;
        }
    }

    public class OrderController : MonoBehaviour
    {
        public OrderEvent MoveOrderEvent;

        //   public OrderEvent SttackOrderEvent;

        //  public OrderEvent MoveOrderEvent;

        private RayCastInfo<ClickObject> LastClickedObject;

        private bool IsOrder()
        {
            LastClickedObject = Helpers.CheckIfObjectIsInFocus<ClickObject>();
            return Input.GetMouseButtonUp(1) && LastClickedObject.Focused;
        }

        private void Update()
        {
            if (IsOrder())
            {

                switch (LastClickedObject.FocusObject.ClickType)
                {
                    case ClickObjectType.Moveable:
                        MoveOrder order = new MoveOrder(LastClickedObject.HitInfo.point);
                        MoveOrderEvent.Raise(order);
                        break;
                    case ClickObjectType.Destructable:
                        break;
                    case ClickObjectType.Attachable:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
