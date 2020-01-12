using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/FlagStateEvent")]
public class FlagStateEvent : ScriptableObject
{
    private List<FlagStateEventListener> listeners = new List<FlagStateEventListener>();

    public void Raise(int gameObject)
    {
        foreach (FlagStateEventListener listener in listeners)
        {
            listener.OnEventRasied(gameObject);
        }
    }

    public void Register(FlagStateEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void Remove(FlagStateEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }
}
