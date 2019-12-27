using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Buildings/Spawn")]
public class BuildSpawnOperation : BuildOperation
{
    public GameObject SpawnObject;

    public override void Activate()
    {
        Debug.Log("BuildSpawnOperation");
    }
}
