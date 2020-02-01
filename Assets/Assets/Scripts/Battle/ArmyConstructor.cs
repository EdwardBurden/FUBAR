using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class ArmyConstructor : MonoBehaviour
    {
        public BasicEvent GroupAddedEvent;

        private int maxsize;

        private List<Group> ArmyGroups;
        private List<ClickObject> UnorganizedObjects;

        [SerializeField]
        private GroupData UnitGroup;

        [SerializeField]
        private GroupData BuildingGroup;

        [SerializeField]
        private Transform SpawnPoint;


        [SerializeField]
        private Transform DynamicContainer;

        public void Init(BattleSettings battleSettings)
        {
            ArmyGroups = new List<Group>();
            maxsize = battleSettings.ArmymaxSize;
        }
        public void AddUnitGroup()
        {
            if (CanCreateGroup())
            {
                Group group = new Group(UnitGroup, SpawnPoint, DynamicContainer);
                ArmyGroups.Add(group); GroupAddedEvent.Raise();
            }
        }

        private bool CanCreateGroup() { return (ArmyGroups.Count < maxsize); }

        public void AddBuildingGroup()
        {
            if (CanCreateGroup())
            {
                Group group = new Group(BuildingGroup, SpawnPoint, DynamicContainer);
                ArmyGroups.Add(group); GroupAddedEvent.Raise();
            }
        }

        public List<Group> Groups() { return ArmyGroups; }

        public List<ClickObject> Objects()
        {
            List<ClickObject> objects = new List<ClickObject>();
            foreach (Group item in ArmyGroups)
            {
                objects.AddRange(item.GetObjects());
            }
            return objects;
        }

    }
}
