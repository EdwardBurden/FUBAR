﻿using System;
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
        public int FormationColumns;
        public int FormationSpace;
        public BaseFormation CurrentFormation;
        public List<Formationenum> Formations;
        public List<GroupOperation> GroupOperations;
        public string Name;
        public LocalGroupData(GroupData data)
        {
            CurrentFormation = new SquareFormation();
            Objects = new List<ClickObject>();
            Icon = data.Icon;
            FormationColumns = data.DefaultColumns;
            FormationSpace = data.DefaultSpace;
            Formations = new List<Formationenum>(data.Formations);
            Name = data.Name;
            GroupOperations = new List<GroupOperation>(data.Operations);
        }
    }

    public class Group
    {
        public LocalGroupData Data;

        public Group(GroupData group, Vector3 position, Quaternion rotation, Transform dynamic)
        {
            Data = new LocalGroupData(group);
            Data.Name += UnityEngine.Random.Range(0, 100);

            CreateGrouo(group, position, rotation, dynamic);
        }

        private void CreateGrouo(GroupData groupData, Vector3 position, Quaternion rotation, Transform dynamic)
        {
            for (int i = 0; i < groupData.ObjectNumber; i++)
            {
                ClickObject obj = GameObject.Instantiate(groupData.Objects, position, rotation, dynamic);
                LocalClickObjectData objectData = new LocalClickObjectData();
                objectData.Name = Data.Name + "Unit" + UnityEngine.Random.Range(0, 100);
                obj.Init(objectData);
                Data.Objects.Add(obj);
            }

            GroupFlag flag = GameObject.Instantiate(groupData.GroupFlag, position, rotation, dynamic);
            flag.Init(this);
            MoveOrder moveOrder = new MoveOrder(position, position, position, false);
            List<Vector3> posList = GetMovementPosition(moveOrder, 0);
            for (int i = 0; i < Data.Objects.Count; i++)
            {
                Data.Objects[i].transform.position = posList[i];
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
                MovementComponent agent = Data.Objects[i].GetComponent<MovementComponent>();
                if (agent)
                    agent.Move(posList[i]);
            }
        }

    }
}