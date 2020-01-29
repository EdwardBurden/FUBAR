using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class ObjectSelectionManager
    {
        private List<ClickObject> SelectObjects;

        public ObjectSelectionManager()
        {
            SelectObjects = new List<ClickObject>();
        }

        public List<ClickObject> GetSelectedObjects()
        {
            return SelectObjects;

        }

        public void ResetSelection()
        {
            foreach (ClickObject obj in SelectObjects)
            {
                if (obj)
                {
                    obj.OffClick();
                }
            }
            SelectObjects.Clear();
        }

        public void NewSelection(ClickObject clickObject)
        {
            ResetSelection();
            clickObject.OnClick();
            SelectObjects.Add(clickObject);
        }


        public void SelectionAdded(ClickObject clickObject)
        {
            clickObject.OnClick();
            SelectObjects.Add(clickObject);
        }

        public void SelectionRemoved(ClickObject clickObject)
        {
            clickObject.OffClick();
            SelectObjects.Remove(clickObject);
        }
    }
}