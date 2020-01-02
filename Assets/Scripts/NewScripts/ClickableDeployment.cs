using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDeployment : MonoBehaviour
{
    public string LocalName;
    public Deployable DeployableRef;
    public GameObjectUnityEvent OnClickEvent;

    public virtual void OnMouseUpAsButton()
    {
        OnClickEvent.Invoke(this.gameObject);
    }


    public virtual void Start()
    {
        LocalName = "Clickable" + Random.Range(0, 100);
    }
}
