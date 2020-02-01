using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class AttachComponent : MonoBehaviour
    {
        private bool Attached;
        private Transform dynamic;

        public void Attach(Transform parent)
        {
            if (!Attached)
                dynamic = this.transform.parent;
            this.transform.parent = parent;
            Attached = true;
        }

        public void Dettach()
        {
            if (Attached)
            {
                this.transform.parent = dynamic;
                Attached = false;
            }
        }
    }
}
