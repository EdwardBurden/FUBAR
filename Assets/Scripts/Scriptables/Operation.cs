using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Operation : ScriptableObject
{
    public Sprite Icon;
    public string Title;
    public string Description;

    public virtual void Activate(MouseSelectable selectable)
    {
        Debug.Log("BuildOperation");

    }

}
