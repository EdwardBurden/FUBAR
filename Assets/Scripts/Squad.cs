using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Squad : MonoBehaviour
{
    private Transform Target;
    public SquadInfo SquadInfo;
    private List<Unit> Units;

    public void Start()
    {
        Units = new List<Unit>();
        Target = FindObjectOfType<Target>().transform;
        InstantiateSquadMembers();
        DelegateMovement();
    }


    public void InstantiateSquadMembers()
    {
        if (SquadInfo != null)
        {
            List<Unit> localunits = new List<Unit>();
            foreach (UnitInfo item in SquadInfo.Units)
            {
                for (int i = 0; i < item.Amount; i++)
                {
                    localunits.Add(item.Unit);
                }
            }


            int Width = localunits.Count / SquadInfo.DefaultDepth;
            int placed = 0;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < SquadInfo.DefaultDepth; j++)
                {
                    Units.Add(Instantiate(localunits[placed], this.transform.position + new Vector3(i * 3, 0, j * 3), Quaternion.identity, this.transform));
                    placed++;
                }
            }


        }
    }

    public void DelegateMovement()
    {
        int Width = Units.Count / SquadInfo.DefaultDepth;
        int placed = 0;

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < SquadInfo.DefaultDepth; j++)
            {
                Units[placed].GetComponent<NavMeshAgent>().SetDestination(Target.position + new Vector3(i * 3, 0, j * 3));
                placed++;
            }
        }

    }

}
