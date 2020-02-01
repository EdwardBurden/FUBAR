using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FUBAR
{
    public class ArmyManagerUI : MonoBehaviour
    {
        [SerializeField]
        private ArmyConstructor Army;

        private int size;

        [SerializeField]
        private ArmyGroupUI ButtonPrefab;

        public void Init(int size)
        {
            this.size = size;
            Fill();
        }

        public void Fill()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            for (int i = 0; i < size; i++)
            {
                ArmyGroupUI button = Instantiate(ButtonPrefab, this.transform);
                if (Army.Groups().Count > i)
                    button.Init(Army.Groups()[i]);
            }
        }

        public void OnGroupUpdate()
        {
            Fill();
        }
    }
}