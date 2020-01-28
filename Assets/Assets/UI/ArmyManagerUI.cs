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

        [SerializeField]
        private ArmyGroupUI ButtonPrefab;

        public void Init()
        {
            foreach (Transform item in this.transform)
            {
                Destroy(item.gameObject);
            }

            foreach (Group item in Army.Groups())
            {
                ArmyGroupUI button = Instantiate(ButtonPrefab, this.transform);
                button.Init(item);
            }
        }

        public void OnGroupUpdate()
        {
            Init();
        }
    }
}