using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickableDeployment : MonoBehaviour
{
    //Refs
    public Group DeployableRef;
    //Data
    public string LocalName;
    public int SpawnRadius;

    //Selection
    public GameObject HoverObject;
    public GameObject ClickObject;

    public abstract void Init(Group deployable);
    public abstract void TriggerOnClick();
    public abstract void TriggerOnAddedToSelection();
    public abstract void TriggerDeselect();
    public abstract void TriggerOnHover();
    public abstract void TriggerOffHover();
}
