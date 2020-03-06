using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUBAR
{
    [Serializable]
    public class ClickObjectListUnityEvent : UnityEvent<List<ClickObject>>
    {
    }

    public class ClickObjectListEventListener : MonoBehaviour
    {
        public ClickObjectListEvent Event;
        public ClickObjectListUnityEvent Response;

        private void OnEnable()
        {
            Event.Register(this);
        }

        private void OnDisable()
        {
            Event.Remove(this);
        }

        public void OnEventRasied(List<ClickObject> clickObject)
        {
            Response.Invoke(clickObject);
        }

    }
}