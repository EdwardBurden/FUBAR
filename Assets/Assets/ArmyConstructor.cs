using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class ArmyConstructor : MonoBehaviour
    {
        public BasicEvent GroupAddedEvent;

        private List<Group> SpawnedGroups;

        [SerializeField]
        private GroupData UnitGroup;

        [SerializeField]
        private GroupData BuildingGroup;

        [SerializeField]
        private Transform SpawnPoint;


        [SerializeField]
        private Transform DynamicContainer;

        public void Init()
        {
            SpawnedGroups = new List<Group>();
        }
        public void AddUnitGroup()
        {
            Group group = new Group(UnitGroup, SpawnPoint, DynamicContainer );
            SpawnedGroups.Add(group); GroupAddedEvent.Raise();
        }

        public void AddBuildingGroup()
        {
            Group group = new Group(BuildingGroup, SpawnPoint, DynamicContainer);
            SpawnedGroups.Add(group); GroupAddedEvent.Raise();
        }

        public List<Group> Groups() { return SpawnedGroups; }

        public List<ClickObject> Objects()
        {
            List<ClickObject> objects = new List<ClickObject>();
            foreach (Group item in SpawnedGroups)
            {
                objects.AddRange(item.GetObjects());
            }
            return objects;
        }

    }
}
