using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class BattleUI : MonoBehaviour
    {
        public ArmyManagerUI ArmyManagerUI;
        public GroupInfoUI GroupUI;
        public ClickObjectListUI ClickObjectUI;

        public void Init(BattleSettings settings)
        {
            ArmyManagerUI.Init(settings.ArmymaxSize);
            GroupUI.Init();
            ClickObjectUI.Init();
        }

        public void SingleGroupSelected(Group group)
        {
            ClickObjectUI.Hide();
            GroupUI.Single(group);

        }

        public void GroupsSelected(Group group)
        {
            ClickObjectUI.Hide();
            GroupUI.Add(group);
        }

        public void GroupDeselected(Group group)
        {
            ClickObjectUI.Hide();
            GroupUI.Remove(group);

        }

        public void ClickObjectSelected(ClickObject click)
        {
            ClickObjectUI.NewSelecetd(click);
            GroupUI.Clear();
        }

        public void NoClickObjectSelected(ClickObject click)
        {
            ClickObjectUI.Hide();
            GroupUI.Clear();
        }

        public void ClickObjectSelectionAdded(List<ClickObject> clicks)
        {
            ClickObjectUI.OnSelecetd(clicks);
            GroupUI.Clear();
        }

        public void ClickObjectSelectionRemoved(ClickObject click)
        {
            ClickObjectUI.OnDeselecetd(click);
            GroupUI.Clear();
        }
    }
}