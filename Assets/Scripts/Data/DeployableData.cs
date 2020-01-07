using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data /DeplyableData")]
public class DeployableData : ScriptableObject
{
    public DeploymentState StartingState;
    public List<Operation> Operations;
    public string DeployableName;
    public int MaxHp;
    public float DeploymentTime;
    public int UnitAmount;
    public int BuildingAmount;

    public GameObject BuildingPrefab;
    public GameObject UnitPrefab;

}
