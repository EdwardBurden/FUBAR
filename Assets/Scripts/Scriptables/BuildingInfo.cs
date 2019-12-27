using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Info/BuildingInfo")]
public class BuildingInfo : ScriptableObject
{
    public string BuildingName = "";
    public List<BuildOperation> BuildOperations;
    public int Hp;
}
