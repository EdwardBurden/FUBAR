﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class GroupSelectionManager
    {
        private List<Group> SelectedGroups;

        public GroupSelectionManager()
        {
            SelectedGroups = new List<Group>();
        }

        public void ResetSelection()
        {
            foreach (Group obj in SelectedGroups)
            {
                if (obj != null)
                {
                    foreach (var item in obj.GetObjects())
                    {
                        item.OffClick();
                    }
                }
            }
            SelectedGroups.Clear();
        }

        public List<Group> GetGroup()
        {
            return SelectedGroups;
        }

        public List<ClickObject> GetAllObjects()
        {
            List<ClickObject> Objects = new List<ClickObject>();
            foreach (var item in SelectedGroups)
            {
                Objects.AddRange(item.GetObjects());
            }
            return Objects;
        }

        public void GroupSelected(Group group)
        {
            ResetSelection();
            SelectedGroups.Add(group);
            foreach (var item in group.GetObjects())
            {
                item.OnClick();
            }
        }

        public void SelectionAdded(Group group)
        {
            SelectedGroups.Add(group);
            foreach (var item in group.GetObjects())
            {
                item.OnClick();
            }
        }

        public void SelectionRemoved(Group groupd)
        {
            SelectedGroups.Remove(groupd);
            foreach (var item in groupd.GetObjects())
            {
                item.OffClick();
            }
        }
    }
}