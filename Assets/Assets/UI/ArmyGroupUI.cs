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

        public Button button;
        public void Click()
        {
            GroupSelectedEvent.Raise(GroupAssigned);
        }

        public void Init(Group group)
        {
            GroupAssigned = group;

            button.image.sprite = group.icon;
        }

    }
}