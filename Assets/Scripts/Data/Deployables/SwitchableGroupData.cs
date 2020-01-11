using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data /SwitchableGroupData")]
public class SwitchableGroupData : DeployableData
{
    public DeploymentState StartingState;
    public int StaticAmount;
    public int MovingAmount;
    public ClickableDeployment StaticPrefab;
    public ClickableDeployment MovingPrefab;
}
