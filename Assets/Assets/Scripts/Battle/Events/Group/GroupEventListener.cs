using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FUBAR { 
[System.Serializable]
public class GroupUnityEvent : UnityEvent<Group>
{
}

    public class GroupEventListener : MonoBehaviour
    {
        public GroupEvent Event;
        public GroupUnityEvent Response;

        private void OnEnable()
        {
            Event.Register(this);
        }

        private void OnDisable()
        {
            Event.Remove(this);
        }

        public void OnEventRasied(Group group)
        {
            Response.Invoke(group);
        }

    }
}
