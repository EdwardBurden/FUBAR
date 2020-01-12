using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Operation : ScriptableObject
{
    public Sprite Icon;
    public string Title;
    public string Description;
    public DeploymentState StateFlag;

    public abstract void Activate(Group selectable);

}
