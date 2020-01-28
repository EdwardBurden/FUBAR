using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class Battle : MonoBehaviour
    {
        public BattleUI UI;
        public ArmyConstructor Army;


        private void Start()
        {
            Army.Init();
            UI.Init();
        }
    }
}