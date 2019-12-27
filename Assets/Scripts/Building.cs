using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MouseSelectable
{
    [SerializeField]
    private BuildingInfo BuildInfo;
    public List<BuildOperation> LocalOperations;
    public Transform SpwanPoint;
    public int HP;
    public string BuildingName;
    private void Start()
    {
        HP = BuildInfo.Hp;
        BuildingName = BuildInfo.BuildingName;
        LocalOperations = new List<BuildOperation>(BuildInfo.BuildOperations);
        foreach (BuildOperation item in LocalOperations)
        {
            item.BuildingRef = this;
        }

    }
}
