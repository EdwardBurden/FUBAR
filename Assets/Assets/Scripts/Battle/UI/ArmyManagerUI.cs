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
        public Transform SpawnPoint;

        [SerializeField]
        private ArmyGroupUI ButtonPrefab;

        public void Init(int size)
        {
            this.size = size;
            Empty();
        }

        private void Empty()
        {
            foreach (Transform item in SpawnPoint)
            {
                Destroy(item.gameObject);
            }
        }

        private void Add()
        {
            ArmyGroupUI button = Instantiate(ButtonPrefab, SpawnPoint);
            button.Init(Army.Groups()[Army.Groups().Count - 1]);
        }

        public void OnGroupUpdate()
        {
            //change to take parameter later
            Add();
        }
    }
}