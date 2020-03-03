using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class ArmyGroupUI : MonoBehaviour
    {
        public GroupEvent GroupSelectedEvent;
        private Group GroupAssigned;
        private bool Selected;


        public Button button;
        public void Click()
        {
            if (GroupAssigned != null)
            {
                GroupSelectedEvent.Raise(GroupAssigned);
            }
        }

        public void Init(Group group)
        {
            GroupAssigned = group;
            Selected = false;
            button.image.sprite = group.Data.Icon;
        }

    }
}