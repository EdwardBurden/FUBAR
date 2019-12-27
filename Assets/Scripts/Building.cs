using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MouseSelectable 
{
    public override void OnMouseUpAsButton()
    {
        CameraController.instance.Follow = false;
        base.OnMouseUpAsButton();
    }
}
