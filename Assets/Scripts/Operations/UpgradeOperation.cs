using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/Upgrade")]
public class UpgradeOperation : Operation
{
    public string NewTitle;
    public ClickableDeployment NewPrefab;
    public List<Operation> UpgradeOperations;

    public override void Activate(Group selectable)
    {
        SwitchableGroup group = selectable.GetComponent<SwitchableGroup>();
        if (group)
        {
            switch (group.LocalData.CurrentState)
            {
                case DeploymentState.Movement:
                    group.LocalData.MovingPrefab = NewPrefab;
                    break;
                case DeploymentState.Grounded:
                    group.LocalData.StaticPrefab = NewPrefab;
                    break;
                default:
                    break;
            }
            group.LocalData.DeployableName = NewTitle;
            group.LocalData.Operations.AddRange(UpgradeOperations);
            group.LocalData.Operations.Remove(this);
            group.ChangeState(group.LocalData.CurrentState);
        }
        else
        {
            StaticGroup staticgroup = selectable.GetComponent<StaticGroup>();
            if (staticgroup)
            {
                staticgroup.LocalData.StaticPrefab = NewPrefab;
                staticgroup.LocalData.DeployableName = NewTitle;
                staticgroup.LocalData.Operations.AddRange(UpgradeOperations);
                staticgroup.LocalData.Operations.Remove(this);
            }
        }


        Debug.Log("BuildUpgradeOperation");
        UIController.instance.Refreshpanels();
    }
}
