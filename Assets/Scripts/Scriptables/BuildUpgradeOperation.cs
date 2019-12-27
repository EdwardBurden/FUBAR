using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Buildings/Upgrade")]
public class BuildUpgradeOperation : BuildOperation
{
    public string NewBuildingTitle;

    public override void Activate()
    {
        Debug.Log("BuildUpgradeOperation");
    }
}
