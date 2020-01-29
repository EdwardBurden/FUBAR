using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class ArmyGroupUI : MonoBehaviour
    {
        public GroupEvent GroupSelectedEvent;
        public GroupEvent GroupRemovedSelectedEvent;
        public GroupEvent GroupAddedSelectedEvent;

        private Group GroupAssigned;
        private bool Selected;


        public Button button;
        public void Click()
        {
            if (GroupAssigned != null)
            {


            }
            GroupSelectedEvent.Raise(GroupAssigned);
        }

        private void Remove()
        {
            GroupRemovedSelectedEvent.Raise(GroupAssigned);
        }

        private void New()
        {
            GroupSelectedEvent.Raise(GroupAssigned);
        }

        private void Add()
        {
            GroupAddedSelectedEvent.Raise(GroupAssigned);
        }



        public void Init(Group group)
        {
            GroupAssigned = group;

            button.image.sprite = group.icon;
        }

    }
}