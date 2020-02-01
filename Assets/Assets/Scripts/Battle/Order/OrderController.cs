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
        }
    }
}
