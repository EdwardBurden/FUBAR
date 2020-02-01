using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class PreviewOrder : Order
    {
        public Vector3 start;
        public Vector3 end;

        public PreviewOrder(Vector3 start, Vector3 end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
