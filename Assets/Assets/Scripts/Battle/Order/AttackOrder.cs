using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class AttackOrder : Order
    {
        public Vector3 Destination;

        public AttackOrder(Vector3 destination)
        {
            Destination = destination;
        }

        public Vector3 GetShootingDistance(Vector3 currentpos)
        {
            return Destination - (Destination - currentpos).normalized * 5;
        }
    }
}