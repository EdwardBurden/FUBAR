using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeployableData : ScriptableObject
{
    public List<Operation> Operations;
    public string DeployableName;
}
