using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{

    [CreateAssetMenu(menuName = "Events/ClickObjectEvent")]
    public class ClickObjectEvent : ScriptableObject
    {
        private List<ClickObjectEventListener> listeners = new List<ClickObjectEventListener>();

        public void Raise(ClickObject clickObject)
        {
            foreach (ClickObjectEventListener listener in listeners)
            {
                listener.OnEventRasied(clickObject);
            }
        }

        public void Register(ClickObjectEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void Remove(ClickObjectEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }
    }
}