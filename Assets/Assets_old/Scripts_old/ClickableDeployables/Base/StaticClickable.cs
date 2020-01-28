using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticClickable : ClickableDeployment
{
    public override void Init(Group deployable)
    {
        DeployableRef = deployable;
    }

    public virtual void Start()
    {
        LocalName = "Static" + Random.Range(0, 100);
    }

    public override void TriggerDeselect()
    {

    }

    public override void TriggerOffHover()
    {

    }

    public override void TriggerOnAddedToSelection()
    {

    }

    public override void TriggerOnClick()
    {

    }

    public override void TriggerOnHover()
    {

    }
}
