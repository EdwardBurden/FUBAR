using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    public static DetectionController instance;

    public List<ClickableDeployment> DetectableObjects;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        DetectableObjects = FindObjectsOfType<ClickableDeployment>().ToList();
        foreach (ClickableDeployment item in DetectableObjects)
        {
            List<ClickableDeployment> potentialtargets = DetectableObjects.Where(x => x.DeployableRef.Side != DeployableSides.Nuetral && item.DeployableRef.Side != x.DeployableRef.Side && Vector3.Distance(x.transform.position, item.transform.position) <= item.SightRange).ToList();
            if (potentialtargets.Count > 0)
            {
                int index = Random.Range(0, potentialtargets.Count);
                item.EnemyDetected(potentialtargets[index]);
            }
            else
                item.EnemyLost();
        }

    }
}


