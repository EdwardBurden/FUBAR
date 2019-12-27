using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildOperation : ScriptableObject
{
    public Building BuildingRef;
    public Sprite Icon;
    public string Title;
    public string Description;

    public virtual void Activate()
    {
        Debug.Log("BuildOperation");

    }

}
