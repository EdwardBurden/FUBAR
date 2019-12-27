using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquadInfo", menuName = "Squad/SquadInfo")]
public class SquadInfo : ScriptableObject
{
    public List<UnitInfo> Units;
    public string SquadName = "";
    public SquadFormation Formation;
    public int DefaultDepth = 3;
}
[Serializable]
public struct UnitInfo
{
    public int Amount;
    public Unit Unit;

}
public enum SquadFormation
{
    Grid = 0,
    Circle = 1,
    Random = 2,
    Hex = 3

}