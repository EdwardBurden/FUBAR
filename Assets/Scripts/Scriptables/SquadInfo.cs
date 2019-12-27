using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Info / SquadInfo")]
public class SquadInfo : BaseInfo
{
    public List<UnitInfo> Units;
    public SquadFormation Formation;
    public int DefaultDepth = 3;
    public float SpawnTime = 0.5f;
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