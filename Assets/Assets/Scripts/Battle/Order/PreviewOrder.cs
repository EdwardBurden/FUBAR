using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class PreviewOrder : Order
    {
        public Vector3 start;
        public Vector3 end;
        public bool Drag;

        public PreviewOrder(Vector3 start, Vector3 end, bool drag)
        {
            this.start = start;
            this.end = end;
            Drag = drag;
        }
    }
}
