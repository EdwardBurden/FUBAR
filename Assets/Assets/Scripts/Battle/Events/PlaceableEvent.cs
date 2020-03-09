using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{

    [CreateAssetMenu(menuName = "Events/PlaceableEvent")]
    public class PlaceableEvent : ScriptableObject
    {
        private List<PlaceableEventListener> listeners = new List<PlaceableEventListener>();

        public void Raise(BasePlacementData clickObject)
        {
            foreach (PlaceableEventListener listener in listeners)
            {
                listener.OnEventRasied(clickObject);
            }
        }

        public void Register(PlaceableEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void Remove(PlaceableEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }
    }
}