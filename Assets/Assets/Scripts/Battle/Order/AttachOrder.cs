using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class AttachOrder : Order
    {
        public GameObject Anchor;

        public AttachOrder(GameObject attachment)
        {
            Anchor = attachment;
        }
    }
}
