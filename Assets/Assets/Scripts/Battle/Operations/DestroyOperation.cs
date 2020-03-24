using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    [CreateAssetMenu(menuName = "Operations/Destroy")]
    public class DestroyOperation : GroupOperation
    {
        public override bool CanUseOperation(Group group)
        {
            return true;
        }

        public override void UseOperation(Group group)
        {

        }
    }
}