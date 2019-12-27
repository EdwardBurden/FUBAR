using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelectable : MonoBehaviour
{
    public GameObjectUnityEvent GameObjectUnityEvent;

    public virtual void OnMouseUpAsButton()
    {
        GameObjectUnityEvent.Invoke(this.gameObject);
    }

}
