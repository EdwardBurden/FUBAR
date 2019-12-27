using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MouseSelectable
{
    public Squad SquadRef;
    public string unitname = "temp";

    private void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (transform.forward * 5), Color.blue);
    }

}
