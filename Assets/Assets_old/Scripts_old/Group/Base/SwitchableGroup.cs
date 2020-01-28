using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DeploymentState
{
    Movement,
    Grounded
}

public class SwitchGroupLocalData
{
    public int StaticAmount;
    public int MovingAmount;
    public ClickableDeployment StaticPrefab;
    public ClickableDeployment MovingPrefab;
    public List<Operation> Operations;
    public string DeployableName;
    public DeploymentState CurrentState;

    public SwitchGroupLocalData() { }

    public SwitchGroupLocalData(int staticAmount, int movingAmount, ClickableDeployment staticPrefab, ClickableDeployment movingPrefab, List<Operation> operations, string deployableName, DeploymentState currentState)
    {
        StaticAmount = staticAmount;
        MovingAmount = movingAmount;
        StaticPrefab = staticPrefab;
        MovingPrefab = movingPrefab;
        Operations = operations;
        DeployableName = deployableName;
        CurrentState = currentState;
    }
}
public abstract class SwitchableGroup : Group
{
    public SwitchGroupLocalData LocalData;

    public override void Start()
    {
        base.Start();
    }

    public void ChangeState(DeploymentState newstate)
    {
        this.transform.position = ClickablesCenterPos();
        ClearClickables();
        LocalData.CurrentState = newstate;
        InitClickables();
    }


    protected override void InitClickables()
    {
        switch (LocalData.CurrentState)
        {
            case DeploymentState.Movement:
                InstatiateChildren(LocalData.MovingPrefab.gameObject, LocalData.MovingAmount);
                break;
            case DeploymentState.Grounded:
                InstatiateChildren(LocalData.StaticPrefab.gameObject, LocalData.StaticAmount);
                break;
            default:
                break;
        }
    }

    protected override void InitData()
    {
        SwitchableGroupData SwitchData = (SwitchableGroupData)BaseData;
        if (SwitchData)
        {
            LocalData = new SwitchGroupLocalData()
            {
                CurrentState = SwitchData.StartingState,
                DeployableName = BaseData.DeployableName,
                MovingAmount = SwitchData.MovingAmount,
                MovingPrefab = SwitchData.MovingPrefab,
                StaticAmount = SwitchData.StaticAmount,
                Operations = new List<Operation>(BaseData.Operations),
                StaticPrefab = SwitchData.StaticPrefab
            };
        }
    }
}
