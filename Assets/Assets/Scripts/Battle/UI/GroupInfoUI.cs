using System;
using System.Collections;
using System.Collections.Generic;
using FUBAR;
using UnityEngine;
using UnityEngine.UI;
namespace FUBAR
{
    public class GroupInfoUI : MonoBehaviour
    {
        public ClickObjectEvent click;
        public Button buttonprefab;
        public Button OperationButtons;
        public Transform ClickList;

        internal void Init()
        {
            //throw new NotImplementedException();
        }

        internal void GroupSelected(FUBAR.Group group)
        {
            foreach (Transform item in ClickList)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in group.GetObjects())
            {
                Button b = Instantiate(buttonprefab, ClickList);
                b.GetComponentInChildren<Text>().text = item.Name;
                b.onClick.AddListener(() => click.Raise(item));
            }

        }
    }
}
