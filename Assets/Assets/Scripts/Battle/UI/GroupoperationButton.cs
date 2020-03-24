using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class GroupoperationButton : MonoBehaviour
    {
        private Group GroupAssigned;
        private GroupOperation Op;

        public void Init(Group g, GroupOperation groupOperation)
        {
            GroupAssigned = g;
            Op = groupOperation;
        }

        public void OnClick()
        {
            if (GroupAssigned != null)
            {
                if (Op.CanUseOperation(GroupAssigned))
                {
                    Op.UseOperation(GroupAssigned);
                }
            }
        }
    }
}