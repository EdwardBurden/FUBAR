using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUBAR
{
    [Serializable]
    public class PlaceableUnityEvent : UnityEvent<BasePlacementData>
    {
    }

    public class PlaceableEventListener : MonoBehaviour
    {
        public PlaceableEvent Event;
        public PlaceableUnityEvent Response;

        private void OnEnable()
        {
            Event.Register(this);
        }

        private void OnDisable()
        {
            Event.Remove(this);
        }

        public void OnEventRasied(BasePlacementData clickObject)
        {
            Response.Invoke(clickObject);
        }

    }
}
