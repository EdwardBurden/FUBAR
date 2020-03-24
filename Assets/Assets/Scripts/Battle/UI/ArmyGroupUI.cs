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
        [SerializeField]
        private GameObject SeperatorPrefab;
        [SerializeField]
        private GroupoperationButton OperationsButtonPrefab;
        [SerializeField]
        private Transform OperationsSpawn;
        public Button button;
        public void GroupIconClick()
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
            button.image.sprite = GroupAssigned.Data.Icon;
            foreach (Transform item in OperationsSpawn)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < GroupAssigned.Data.GroupOperations.Count; i++)
            {
                GroupoperationButton btn = Instantiate(OperationsButtonPrefab, OperationsSpawn);
                btn.Init(GroupAssigned, GroupAssigned.Data.GroupOperations[i]);
                if (i < GroupAssigned.Data.GroupOperations.Count - 1)
                    Instantiate(SeperatorPrefab, OperationsSpawn);
            }
        }

    }
}