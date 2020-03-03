using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{

    public class MoveOrder : Order
    {
        public Vector3 Start;
        public Vector3 End;
        public Vector3 Destination;
        public bool DragOrder;

        public MoveOrder(Vector3 destination, Vector3 start, Vector3 end, bool drag)
        {
            Destination = destination;
            Start = start;
            End = end;
            DragOrder = drag;
        }
    }
}
