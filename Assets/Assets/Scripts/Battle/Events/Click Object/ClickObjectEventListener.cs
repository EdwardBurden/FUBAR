using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUBAR
{
    [Serializable]
    public class ClickObjectUnityEvent : UnityEvent<ClickObject>
    {
    }

    public class ClickObjectEventListener : MonoBehaviour
    {
        public ClickObjectEvent Event;
        public ClickObjectUnityEvent Response;

        private void OnEnable()
        {
            Event.Register(this);
        }

        private void OnDisable()
        {
            Event.Remove(this);
        }

        public void OnEventRasied(ClickObject clickObject)
        {
            Response.Invoke(clickObject);
        }

    }
}
