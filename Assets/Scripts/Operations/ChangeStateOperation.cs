using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/ChangeState")]
public class ChangeStateOperation : Operation
{
    public DeploymentState DeploymentState;

    public override void Activate(Deployable selectable)
    {
        SwitchableGroup group = selectable.GetComponent<SwitchableGroup>();
        if (group)
        {
            group.ChangeState(DeploymentState);
            UIController.instance.Refreshpanels();
        }
    }
}
