using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableDeployment : MonoBehaviour
{
    public string LocalName;
    public Deployable DeployableRef;
    public int SightRange = 5;
    public int Health = 500;

    public GameObject HoverObject;
    public GameObject ClickObject;

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
        if (ClickObject)
            ClickObject.SetActive(true);
    }

    public virtual void TriggerOnAddedToSelection()
    {
        if (ClickObject)
            ClickObject.SetActive(true);
    }

    public virtual void TriggerDeselect()
    {
        if (ClickObject)
            ClickObject.SetActive(false);
    }

    public virtual void TriggerOnHover()
    {
        if (HoverObject)
            HoverObject.SetActive(true);
    }

    public virtual void TriggerOffHover()
    {
        if (HoverObject)
            HoverObject.SetActive(false);
    }
}
