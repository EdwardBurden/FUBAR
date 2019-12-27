using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Upgrade")]
public class UpgradeOperation : Operation
{
    public string NewBuildingTitle;
    public List<Operation> UpgradeOperations;
    public int MaxHp;

    public override void Activate(MouseSelectable selectable)
    {
        selectable.LocalOperations.AddRange(UpgradeOperations);
        selectable.LocalOperations.Remove(this);
        selectable.HP = MaxHp;
        selectable.SelectableName = NewBuildingTitle;
        Debug.Log("BuildUpgradeOperation");
        UIController.instance.Refreshpanels();
    }
}
