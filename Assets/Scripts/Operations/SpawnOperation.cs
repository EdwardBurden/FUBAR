using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Spawn")]
public class SpawnOperation : Operation
{
    public GameObject SpawnObject;

    public override void Activate(Deployable selectable)
    {
        Instantiate(SpawnObject, selectable.SpawnPoint.position, selectable.SpawnPoint.rotation, null);
    }
}
