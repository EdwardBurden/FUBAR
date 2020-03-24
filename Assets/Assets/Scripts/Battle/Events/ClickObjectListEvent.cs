using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    [CreateAssetMenu(menuName = "Events/ClickObjectListEvent")]
    public class ClickObjectListEvent : ScriptableObject
    {
        private List<ClickObjectListEventListener> listeners = new List<ClickObjectListEventListener>();

        public void Raise(List<ClickObject> clickObjects)
        {
            foreach (ClickObjectListEventListener listener in listeners)
            {
                listener.OnEventRasied(clickObjects);
            }
        }

        public void Register(ClickObjectListEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void Remove(ClickObjectListEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }
    }
}