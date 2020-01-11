using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data /StaticGroupData")]
public class StaticGroupData : DeployableData
{
    public int StaticAmount;
    public ClickableDeployment StaticPrefab;
}
