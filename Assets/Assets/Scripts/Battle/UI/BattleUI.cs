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

        public void ClickObjectSelected(ClickObject click)
        {
            ClickObjectUI.NewSelecetd(click);
        }

        public void NoClickObjectSelected(ClickObject click)
        {
            ClickObjectUI.Hide();
        }

        public void ClickObjectSelectionAdded(List<ClickObject> clicks)
        {
            ClickObjectUI.OnSelecetd(clicks);
        }

        public void ClickObjectSelectionRemoved(ClickObject click)
        {
            ClickObjectUI.OnDeselecetd(click);
        }
    }
}