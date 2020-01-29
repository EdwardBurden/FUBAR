using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    [CreateAssetMenu(menuName = "Events/OrderEvent")]
    public class OrderEvent : ScriptableObject
    {
        private List<OrderEventListener> listeners = new List<OrderEventListener>();

        public void Raise(Order order)
        {
            foreach (OrderEventListener listener in listeners)
            {
                listener.OnEventRasied(order);
            }
        }

        public void Register(OrderEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void Remove(OrderEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }
    }
}