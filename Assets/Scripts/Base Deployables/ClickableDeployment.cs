using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDeployment : MonoBehaviour
{
    public string LocalName;
    public Deployable DeployableRef;
    public GameObjectUnityEvent OnClickEvent;
    public int SightRange = 5;

    public virtual void OnMouseUpAsButton()
    {
        OnClickEvent.Invoke(this.gameObject);
    }

    public virtual void Start()
    {
        LocalName = "Clickable" + Random.Range(0, 100);
    }

    public virtual void EnemyDetected(ClickableDeployment enemy) { }

    public virtual void EnemyLost(ClickableDeployment enemy) { }
}
