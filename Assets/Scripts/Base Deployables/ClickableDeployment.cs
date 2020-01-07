using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDeployment : MonoBehaviour
{
    public string LocalName;
    public Deployable DeployableRef;
    public GameObjectUnityEvent OnClickEvent;
    public GameObjectUnityEvent OnAddedToSelectionEvent;
    public GameObjectUnityEvent OnDeselectEvent;
    public GameObjectUnityEvent OnHoverEvent;
    public GameObjectUnityEvent OffHoverEvent;
    public int SightRange = 5;
    public int Health = 500;


    public virtual void Start()
    {
        LocalName = "Clickable" + Random.Range(0, 100);
    }

    public virtual void Update()
    {

    }

    public virtual void EnemyDetected(ClickableDeployment enemy) { }

    public virtual void EnemyLost() { }

    public virtual void TriggerOnClick()
    {
        OnClickEvent.Invoke(this.gameObject);
    }

    public virtual void TriggerOnAddedToSelection()
    {
        OnAddedToSelectionEvent.Invoke(this.gameObject);
    }

    public virtual void TriggerDeselect()
    {
        OnDeselectEvent.Invoke(this.gameObject);
    }

    public virtual void TriggerOnHover()
    {
        OnHoverEvent.Invoke(this.gameObject);
    }

    public virtual void TriggerOffHover()
    {
        OffHoverEvent.Invoke(this.gameObject);
    }

}
