using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupData : ScriptableObject
{
    public List<Operation> Operations;
    public string DeployableName;
    public int GroupSpawnRadius;
}
