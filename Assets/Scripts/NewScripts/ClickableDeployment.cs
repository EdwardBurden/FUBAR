using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDeployment : MonoBehaviour
{
    public string localitemname;
    public Deployable DeployableRef;
    public GameObjectUnityEvent OnClickEvent;

    public virtual void OnMouseUpAsButton()
    {
        OnClickEvent.Invoke(this.gameObject);
    }
}
