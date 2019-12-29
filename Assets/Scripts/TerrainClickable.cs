using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainClickable : MonoBehaviour
{
    public GameObjectUnityEvent OnClickEvent;

    public virtual void OnMouseUpAsButton()
    {
        OnClickEvent.Invoke(this.gameObject);
    }
}
