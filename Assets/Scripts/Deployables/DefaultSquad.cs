using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSquad : Deployable
{
    public GameObject flag;

    private void Update()
    {
        flag.transform.position = new Vector3( ClickablesCenterPos.x, flag.transform.position.y , ClickablesCenterPos.z);
    }
}
