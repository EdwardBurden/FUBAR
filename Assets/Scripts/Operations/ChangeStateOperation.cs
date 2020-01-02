using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Operations/ChangeState")]
public class ChangeStateOperation : Operation
{
    public DeploymentState DeploymentState;

    public override void Activate(Deployable selectable)
    {
        selectable.ChangeState(DeploymentState);
        UIController.instance.Refreshpanels();
    }
}
