using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Terrain : MonoBehaviour
{
    public UnityEvent Event;
    private void OnMouseUpAsButton()
    {
        Event.Invoke();
    }
}
