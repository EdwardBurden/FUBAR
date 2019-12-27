using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Buildings/Spawn")]
public class BuildSpawnOperation : BuildOperation
{
    public GameObject SpawnObject;

    public override void Activate()
    {
        Instantiate(SpawnObject, BuildingRef.SpwanPoint.position, BuildingRef.SpwanPoint.rotation, null);
    }
}
