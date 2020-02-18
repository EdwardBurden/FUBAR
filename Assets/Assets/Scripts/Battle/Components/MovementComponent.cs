using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementComponent : MonoBehaviour
    {
        public float Spacing;
        private NavMeshAgent Agent;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        public void Move(Vector3 target)
        {
            Agent.SetDestination(target);
        }
    }
}