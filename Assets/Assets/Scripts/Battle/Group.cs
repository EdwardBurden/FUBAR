using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{
    public class Group
    {
        private List<ClickObject> Objects;
        private GroupData Data;
        public Sprite icon;
        public Formation Formation;
        public List<Operation> Operations;

        public int columns = 6;
        public int space = 3;

        public Group(GroupData group, Transform transform, Transform dynamic)
        {
            Formation = Formation.Square;
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

        public void Move(MoveOrder order)
        {
            List<NavMeshAgent> moveable = new List<NavMeshAgent>();
            List<Vector3> positions = new List<Vector3>();
            foreach (ClickObject click in Objects)
            {
                NavMeshAgent agent = click.GetComponent<NavMeshAgent>();
                if (agent)
                    moveable.Add(agent);
            }

            switch (Formation)
            {
                case Formation.Square:
                    positions = Formations.OrganiseSquareFormation(order.Destination, moveable , columns,space);
                    break;
                case Formation.Line:
                    break;
                default:
                    break;
            }

            for (int i = 0; i < moveable.Count; i++)
            {
                AttachComponent comp = moveable[i].GetComponent<AttachComponent>();
                if (comp)
                    comp.Dettach();
                moveable[i].SetDestination(positions[i]);
            }
        }


        public List<ClickObject> GetObjects() { return Objects; }
    }
}