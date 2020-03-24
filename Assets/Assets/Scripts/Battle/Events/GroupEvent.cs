using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{

    [CreateAssetMenu(menuName = "Events/GroupEvent")]
    public class GroupEvent : ScriptableObject
    {
        private List<GroupEventListener> listeners = new List<GroupEventListener>();

        public void Raise(Group group)
        {
            foreach (GroupEventListener listener in listeners)
            {
                listener.OnEventRasied(group);
            }
        }

        public void Register(GroupEventListener eventListener)
        {
            listeners.Add(eventListener);
        }

        public void Remove(GroupEventListener eventListener)
        {
            listeners.Remove(eventListener);
        }
    }
}