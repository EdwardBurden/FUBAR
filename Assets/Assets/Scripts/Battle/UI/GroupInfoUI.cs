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
        public Button ClickUnitPrefab;
        public Button FormationChangeButtonPrefab;

        public Transform ClickList;
        public Transform Formations;

        private List<Group> Selected;

        internal void Init()
        {
            Selected = new List<Group>();
            //throw new NotImplementedException();
        }

        public void Clear()
        {
            Selected.Clear();
            foreach (Transform item in ClickList)
            {
                Destroy(item.gameObject);
            }
            foreach (Transform item in Formations)
            {
                Destroy(item.gameObject);
            }
        }

        public void Add(Group group)
        {

            Clear();

        }

        public void Remove(Group group)
        {

            Clear();

        }


        public void Single(FUBAR.Group group)
        {
            Clear();
            foreach (var item in group.GetObjects())
            {
                Button b = Instantiate(ClickUnitPrefab, ClickList);
                b.GetComponentInChildren<Text>().text = item.Data.Name;
                b.onClick.AddListener(() => click.Raise(item));
            }
            foreach (Formationenum foo in group.Data.Formations)
            {
                Button b = Instantiate(FormationChangeButtonPrefab, Formations);
                switch (foo)
                {
                    case Formationenum.Square:
                        b.onClick.AddListener(() => group.ChangeFormation(new SquareFormation()));
                        break;
                    case Formationenum.Line:
                      //  b.onClick.AddListener(() => group.ChangeFormation(foo));
                        break;
                    default:
                        break;
                }
            }
            Selected.Add(group);
        }
    }
}
