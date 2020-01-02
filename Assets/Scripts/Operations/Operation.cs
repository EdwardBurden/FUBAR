using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Operation : ScriptableObject
{
    public Sprite Icon;
    public string Title;
    public string Description;
    public DeploymentState StateFlag;

    public virtual void Activate(Deployable selectable)
    {
        Debug.Log("BuildOperation");
    }

}
