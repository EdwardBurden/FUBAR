using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class BattleUI : MonoBehaviour
    {
        public ArmyManagerUI ArmyManagerUI;
        public GroupInfoUI GroupUI;

        public void Init()
        {
            ArmyManagerUI.Init(); 
            GroupUI.Init();
        }

        public void OnGroupSelected(Group group)
        {
            GroupUI.GroupSelected(group);

        }
    }
}