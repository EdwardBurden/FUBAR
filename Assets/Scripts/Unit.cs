using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public GameObjectUnityEvent UnitSelected;
    private void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (transform.forward * 5), Color.blue);
    }


    private void OnMouseUpAsButton()
    {
        UnitSelected.Invoke(this.gameObject);
    }
}
