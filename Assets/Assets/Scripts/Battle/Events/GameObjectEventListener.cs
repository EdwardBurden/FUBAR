using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameObjectUnityEvent : UnityEvent<GameObject>
{
}

public class GameObjectEventListener : MonoBehaviour
{
    public GameObjectEvent Event;
    public GameObjectUnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.Remove(this);
    }

    public void OnEventRasied(GameObject gameObject)
    {
        Response.Invoke(gameObject);
    }


}
