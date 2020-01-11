using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaticGroupLocalData
{
    public int StaticAmount;
    public ClickableDeployment StaticPrefab;
    public List<Operation> Operations;
    public string DeployableName;

    public StaticGroupLocalData() { }

    public StaticGroupLocalData(int staticAmount, ClickableDeployment staticPrefab, List<Operation> operations, string deployableName)
    {
        StaticAmount = staticAmount;
        StaticPrefab = staticPrefab;
        Operations = operations;
        DeployableName = deployableName;
    }
}
public class StaticGroup : Deployable
{
    public StaticGroupLocalData LocalData;

    protected override void InitClickables()
    {
        InstatiateClickables(LocalData.StaticPrefab, LocalData.StaticAmount);
    }

    protected override void InitData()
    {
        StaticGroupData StaticData = (StaticGroupData)BaseData;
        if (StaticData)
        {
            LocalData = new StaticGroupLocalData()
            {
                DeployableName = StaticData.DeployableName,
                StaticAmount = StaticData.StaticAmount,
                Operations = new List<Operation>(StaticData.Operations),
                StaticPrefab = StaticData.StaticPrefab
            };
        }
    }
}

