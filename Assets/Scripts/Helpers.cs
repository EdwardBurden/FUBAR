﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static RayCastInfo<T> CheckIfObjectIsInFocus<T>() where T : MonoBehaviour
    {
        RaycastHit hit = new RaycastHit();
        bool inSight = false;
        T foundObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance = 10000;
        RaycastHit[] raycasts = Physics.RaycastAll(ray);
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].distance < distance)
            {
                distance = raycasts[i].distance;
                T foundobject = raycasts[i].collider.gameObject.GetComponent<T>();
                if (foundobject)
                {
                    inSight = true;
                    hit = raycasts[i];
                    foundObject = foundobject;
                }
                else
                    inSight = false;
            }
        }
        return new RayCastInfo<T>(inSight, hit, foundObject);
    }
}
