using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{

    public class MoveOrder : Order
    {
        public Vector3 Destination;

        public MoveOrder(Vector3 destination)
        {
            Destination = destination;
        }
    }
}
