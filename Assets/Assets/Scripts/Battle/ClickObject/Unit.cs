using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{
    public class Unit : ClickObject
    {


        private void OnDrawGizmos()
        {
            //Gizmos.DrawCube(GetComponent<NavMeshAgent>().nextPosition, Vector3.one);
              for (int i = 0; i < GetComponent<NavMeshAgent>().path.corners.Length; i++)
              {

                  Gizmos.DrawCube(GetComponent<NavMeshAgent>().path.corners[i], Vector3.one);
              }
          
        }
    }


}