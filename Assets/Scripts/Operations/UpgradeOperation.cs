using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Upgrade")]
public class UpgradeOperation : Operation
{
    public string NewBuildingTitle;
    public GameObject NewBuildingPrefab;
    public List<Operation> UpgradeOperations;
    public int MaxHp;

    public override void Activate(Deployable selectable)
    {
        selectable.LocalOperations.AddRange(UpgradeOperations);
        selectable.LocalOperations.Remove(this);
        selectable.HP = MaxHp;
        selectable.DeploymentName = NewBuildingTitle;
        selectable.BuildingPrefab = NewBuildingPrefab;
        selectable.ChangeState(selectable.CurrentState);
        Debug.Log("BuildUpgradeOperation");
        UIController.instance.Refreshpanels();
    }
}
