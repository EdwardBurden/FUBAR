using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private Transform Target;
    private NavMeshAgent Agent;
    // Start is called before the first frame update
    void Start()
    {
        Target = FindObjectOfType<Target>().transform;
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Target.position);
    }
}
