using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{

    public enum Formation
    {
        Square,
        Line
    }

    public class Group
    {
        private List<ClickObject> Objects;
        private GroupData Data;
        public Sprite icon;
        public Formation Formation;
        public List<Operation> Operations;

        public Group(GroupData group, Transform transform, Transform dynamic)
        {

            Objects = new List<ClickObject>();
            Data = group;
            icon = Data.Icon;
            Operations = Data.Operations;
            for (int i = 0; i < Data.ObjectNumber; i++)
            {
                ClickObject obj = GameObject.Instantiate(Data.Objects, transform.position, transform.rotation, dynamic);
                obj.Init(this);
                Objects.Add(obj);
            }
        }

        public List<ClickObject> GetObjects() { return Objects; }
    }
}