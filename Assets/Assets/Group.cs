using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

        public int columns = 4;
        public int space = 10;

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
                    positions = OrganiseSquareFormation(order.Destination, moveable);
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





        public List<Vector3> OrganiseSquareFormation(Vector3 start, List<NavMeshAgent> agents)
        {
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < agents.Count; i++)
            {
                Vector3 pos = CalcPosition(i);
                positions.Add(new Vector3(start.x + pos.x, 0, start.z + pos.y));
            }
            return positions;
        }




        Vector2 CalcPosition(int index) // call this func for all your objects
        {
            float posX = (index % columns) * space;
            float posY = (index / columns) * space;
            return new Vector2(posX, posY);
        }


        public List<ClickObject> GetObjects() { return Objects; }
    }
}