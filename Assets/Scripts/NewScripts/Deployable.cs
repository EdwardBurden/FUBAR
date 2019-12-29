using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum DeploymentState
{
    Movement,
    Static
}

public class Deployable : MonoBehaviour
{
    public DeploymentState CurrentState;
    public List<ClickableDeployment> ClickableDeployments;
    public DeployableData Data;
    public List<Operation> LocalOperations;
    public int HP;
    public string DeploymentName;
    public Vector3 SpawnPointOffset;
    public GameObject BuildingPrefab;
    public Vector3 ClickablesCenterPos
    {
        get
        {
            if (ClickableDeployments != null && ClickableDeployments.Count > 0)
            {
                Vector3 total = Vector3.zero;
                foreach (var item in ClickableDeployments)
                {
                    total += item.gameObject.transform.position;
                }
                return total / (float)ClickableDeployments.Count;
            }
            else return this.transform.position;
        }
    }
    public Transform SpawnPoint
    {
        get
        {
            Transform transform = this.transform;
            transform.localPosition += SpawnPointOffset;
            return transform;
        }
    }

    private void Start()
    {
        ClickableDeployments = new List<ClickableDeployment>();

        InitData();
        InitClickables();
    }
    public void InitClickables()
    {
        switch (CurrentState)
        {
            case DeploymentState.Movement:
                InstatiateUnits();
                break;
            case DeploymentState.Static:
                InstatiateBuildings();
                break;
            default:
                break;
        }

    }

    public void ClearClickables()
    {
        foreach (var item in ClickableDeployments)
        {
            Destroy(item.gameObject);
        }
        ClickableDeployments.Clear();
    }

    public void ChangeState(DeploymentState newstate)
    {
        this.transform.position = ClickablesCenterPos;
        ClearClickables();
        CurrentState = newstate;
        InitClickables();
    }

    public void InitData()
    {
        if (Data)
        {
            LocalOperations = new List<Operation>(Data.Operations);
            HP = Data.MaxHp;
            DeploymentName = Data.DeployableName;
            CurrentState = Data.StartingState;
            BuildingPrefab = Data.BuildingPrefab;
        }
    }

    public void InstatiateBuildings()
    {
        if (Data != null)
        {
            for (int i = 0; i < Data.BuildingAmount; i++)
            {
                ClickableDeployment building = Instantiate(BuildingPrefab, this.transform.position + new Vector3((i * 3) - ((Data.BuildingAmount - 1) * 3) / 2.0f, 0, 0), Quaternion.identity, this.transform).GetComponent<ClickableDeployment>();
                building.DeployableRef = this;
                ClickableDeployments.Add(building);
            }
        }
    }

    //keep
    public void InstatiateUnits()
    {
        if (Data != null)
        {
            for (int i = 0; i < Data.UnitAmount; i++)
            {
                ClickableDeployment building = Instantiate(Data.UnitPrefab, this.transform.position + new Vector3((i * 3)-((Data.UnitAmount-1)*3)/2.0f, 0, 0), Quaternion.identity, this.transform).GetComponent<ClickableDeployment>();
                building.DeployableRef = this;
                ClickableDeployments.Add(building);
            }
        }
        //if (Data != null)
        //{
        //    List<Unit> localunits = new List<Unit>();
        //    foreach (UnitInfo item in SquadInfo.Units)
        //    {
        //        for (int i = 0; i < item.Amount; i++)
        //        {
        //            localunits.Add(item.Unit);
        //        }
        //    }
        //    int Width = localunits.Count / SquadInfo.DefaultDepth;
        //    int placed = 0;

        //    for (int i = 0; i < Width; i++)
        //    {
        //        for (int j = 0; j < SquadInfo.DefaultDepth; j++)
        //        {
        //            Unit unit = Instantiate(localunits[placed], this.transform.position + new Vector3(i * 3, 0, j * 3), Quaternion.identity, this.transform);
        //            unit.SquadRef = this;
        //            Units.Add(unit);
        //            placed++;
        //        }
        //    }
        //}
    }

    //creaet operations
    public void MoveToTarget(Vector3 target)
    {
        for (int i = 0; i < ClickableDeployments.Count; i++)
        {
            if (ClickableDeployments[i].GetComponent<Unit>() && ClickableDeployments[i].GetComponent<NavMeshAgent>())
            {
                ClickableDeployments[i].GetComponent<NavMeshAgent>().SetDestination(target + new Vector3(i * 3, 0, 0));
            }
        }

        //int Width = Units.Count / SquadInfo.DefaultDepth;
        //int placed = 0;

        //for (int i = 0; i < Width; i++)
        //{
        //    for (int j = 0; j < SquadInfo.DefaultDepth; j++)
        //    {
        //        Units[placed].GetComponent<NavMeshAgent>().SetDestination(target + new Vector3(i * 3, 0, j * 3));
        //        placed++;
        //    }
        //}
    }
}
