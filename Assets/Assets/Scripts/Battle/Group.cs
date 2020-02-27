using System;
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

        public List<Operation> Operations;

        //formation data
        private int FormationColumns;
        private int FormationSpace;

        public BaseFormation CurrentFormation;
        public List<Formationenum> Formations;

        public Group(GroupData group, Transform transform, Transform dynamic)
        {
            CurrentFormation = new SquareFormation();
            Objects = new List<ClickObject>();
            Data = group;
            icon = Data.Icon;
            Operations = Data.Operations;
            FormationColumns = Data.DefaultColumns;
            FormationSpace = Data.DefaultSpace;
            Formations = Data.Formations;
            for (int i = 0; i < Data.ObjectNumber; i++)
            {
                ClickObject obj = GameObject.Instantiate(Data.Objects, transform.position, transform.rotation, dynamic);
                obj.Init(this);
                Objects.Add(obj);
            }
        }

        public List<ClickObject> GetObjects() { return Objects; }
        public List<Vector3> GetMovementPreviewPosition(PreviewOrder moveOrder, int order)
        {
            if (moveOrder.Drag)
            {
                FormationColumns = CurrentFormation.GetColumnsFromDistance(moveOrder.start, moveOrder.end, FormationSpace);
                return CurrentFormation.GetDragFormationPosition(moveOrder.start, moveOrder.end, Objects, FormationColumns, FormationSpace);
            }
            else
                return CurrentFormation.GetFormationPosition(moveOrder.end, Objects, FormationColumns, FormationSpace);
        }

        internal void ChangeFormation(BaseFormation newFormation)
        {
            CurrentFormation = newFormation;
            //
        }

        public List<Vector3> GetMovementPosition(MoveOrder moveOrder, int order)
        {
            if (moveOrder.DragOrder)
            {
                FormationColumns = CurrentFormation.GetColumnsFromDistance(moveOrder.Start, moveOrder.End , FormationSpace);
                return CurrentFormation.GetDragFormationPosition(moveOrder.Start, moveOrder.End, Objects, FormationColumns, FormationSpace);
            }
            else
                return CurrentFormation.GetFormationPosition(moveOrder.Destination, Objects, FormationColumns, FormationSpace);
        }

        public void Move(List<Vector3> posList)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                AttachComponent comp = Objects[i].GetComponent<AttachComponent>();
                if (comp)
                    comp.Dettach();
                MovementComponent agent = Objects[i].GetComponent<MovementComponent>();
                if (agent)
                    agent.Move(posList[i]);
            }
        }

    }
}