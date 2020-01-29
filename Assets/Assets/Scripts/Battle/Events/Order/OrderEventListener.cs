using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUBAR
{
    [System.Serializable]
    public class OrderUnityEvent : UnityEvent<Order>
    {
    }

    public class OrderEventListener : MonoBehaviour
    {
        public OrderEvent Event;
        public OrderUnityEvent Response;

        private void OnEnable()
        {
            Event.Register(this);
        }

        private void OnDisable()
        {
            Event.Remove(this);
        }

        public void OnEventRasied(Order order)
        {
            Response.Invoke(order);
        }

    }
}
