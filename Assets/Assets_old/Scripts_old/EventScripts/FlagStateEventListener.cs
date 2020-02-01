using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FlagStatetUnityEvent : UnityEvent<int>
{
}

public class FlagStateEventListener : MonoBehaviour
{
    public FlagStateEvent Event;
    public FlagStatetUnityEvent Response;

    private void OnEnable()
    {
        Event.Register(this);
    }

    private void OnDisable()
    {
        Event.Remove(this);
    }

    public void OnEventRasied(int gameObject)
    {
        Response.Invoke(gameObject);
    }


}
