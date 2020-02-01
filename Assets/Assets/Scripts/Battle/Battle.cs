using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    [Serializable]
    public class BattleSettings
    {
        public int ArmymaxSize = 20;

    }

    public class Battle : MonoBehaviour
    {
        public SelectionController SelectionController;
        public PlacementController PlacementController;
        public OrderController OrderController;

        public BattleUI UI;
        public ArmyConstructor Army;
        public BattleSettings Settings;

        private void Start()
        {
            SelectionController.Init();
            Army.Init(Settings);
            UI.Init(Settings);
        }
    }
}