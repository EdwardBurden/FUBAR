using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{

    public class LocalGroupData
    {
        public List<ClickObject> Objects;
        public Sprite Icon;
        public List<Operation> Operations;
        public int FormationColumns;
        public int FormationSpace;
        public BaseFormation CurrentFormation;
        public List<Formationenum> Formations;
        public string Name;
        public LocalGroupData(GroupData data)
        {
            CurrentFormation = new SquareFormation();
            Objects = new List<ClickObject>();
            Icon = data.Icon;
            Operations = data.Operations;
            FormationColumns = data.DefaultColumns;
            FormationSpace = data.DefaultSpace;
            Formations = data.Formations;
            Name = data.Name;
        }
    }



    public class Group
    {
        public LocalGroupData Data;
        private GroupData StartData;

        public Group(GroupData group, Transform transform, Transform dynamic)
        {
            Data = new LocalGroupData(group);
            Data.Name += UnityEngine.Random.Range(0, 100);
            for (int i = 0; i < group.ObjectNumber; i++)
            {
                ClickObject obj = GameObject.Instantiate(group.Objects, transform.position, transform.rotation, dynamic);
                LocalClickObjectData objectData = new LocalClickObjectData();
                objectData.Name = Data.Name + "Unit" + UnityEngine.Random.Range(0, 100);
                obj.Init(objectData);
                Data.Objects.Add(obj);
            }
        }

        public List<ClickObject> GetObjects() { return Data.Objects; }
        public List<Vector3> GetMovementPreviewPosition(PreviewOrder moveOrder, int order)
        {
            if (moveOrder.Drag)
            {
                Data.FormationColumns = Data.CurrentFormation.GetColumnsFromDistance(moveOrder.start, moveOrder.end, Data.FormationSpace);
                return Data.CurrentFormation.GetDragFormationPosition(moveOrder.start, moveOrder.end, Data.Objects, Data.FormationColumns, Data.FormationSpace);
            }
            else
                return Data.CurrentFormation.GetFormationPosition(moveOrder.end, Data.Objects, Data.FormationColumns, Data.FormationSpace);
        }

        internal void ChangeFormation(BaseFormation newFormation)
        {
            Data.CurrentFormation = newFormation;
        }

        public List<Vector3> GetMovementPosition(MoveOrder moveOrder, int order)
        {
            if (moveOrder.DragOrder)
            {
                Data.FormationColumns = Data.CurrentFormation.GetColumnsFromDistance(moveOrder.Start, moveOrder.End, Data.FormationSpace);
                return Data.CurrentFormation.GetDragFormationPosition(moveOrder.Start, moveOrder.End, Data.Objects, Data.FormationColumns, Data.FormationSpace);
            }
            else
                return Data.CurrentFormation.GetFormationPosition(moveOrder.Destination, Data.Objects, Data.FormationColumns, Data.FormationSpace);
        }

        public void Move(List<Vector3> posList)
        {
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                AttachComponent comp = Data.Objects[i].GetComponent<AttachComponent>();
                if (comp)
                    comp.Dettach();
                MovementComponent agent = Data.Objects[i].GetComponent<MovementComponent>();
                if (agent)
                    agent.Move(posList[i]);
            }
        }

    }
}