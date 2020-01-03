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
            foreach (ClickableDeployment enemy in DetectableObjects.Where(x => x.DeployableRef.Side != DeployableSides.Nuetral && item.DeployableRef.Side != x.DeployableRef.Side))
            {
                if (Vector3.Distance(enemy.transform.position, item.transform.position) <= item.SightRange)
                    item.EnemyDetected(enemy);
                else
                    item.EnemyLost(enemy);
            }
        }
    }
}

