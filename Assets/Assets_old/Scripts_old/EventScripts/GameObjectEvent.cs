using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/GameObjectEvent")]
public class GameObjectEvent : ScriptableObject
{
    private List<GameObjectEventListener> listeners = new List<GameObjectEventListener>();

    public void Raise(GameObject gameObject)
    {
        foreach (GameObjectEventListener listener in listeners)
        {
            listener.OnEventRasied(gameObject);
        }
    }

    public void Register(GameObjectEventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    public void Remove(GameObjectEventListener eventListener)
    {
        listeners.Remove(eventListener);
    }
}
