using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Buildings/Upgrade")]
public class BuildUpgradeOperation : BuildOperation
{
    public string NewBuildingTitle;
    public List<BuildOperation> UpgradeOperations;
    public int MaxHp;

    public override void Activate()
    {
        BuildingRef.LocalOperations.AddRange(UpgradeOperations);
        BuildingRef.LocalOperations.Remove(this);
        BuildingRef.HP = MaxHp;
        Debug.Log("BuildUpgradeOperation");
        UIController.instance.RefreshBuildPanel();
    }
}
