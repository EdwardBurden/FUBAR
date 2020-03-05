using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    public class ArmyGroup
    {
        private List<Group> Groups;
        private List<ClickObject> Objects;

        public ArmyGroup(List<Group> groups, List<ClickObject> objects)
        {
            Groups = groups;
            Objects = objects;
        }

        public ArmyGroup()
        {
            Groups = new List<Group>();
            Objects = new List<ClickObject>();
        }

        public List<ClickObject> GetObjects()
        {
            List<ClickObject> objects = new List<ClickObject>();
            foreach (Group item in Groups)
            {
                objects.AddRange(item.GetObjects());
            }
            objects.AddRange(Objects);
            return objects;
        }

        public List<Group> GetGroups()
        {
            return Groups;
        }

        public void Add(ClickObject clickObject)
        {
            Objects.Add(clickObject);
        }

        public void Add(Group group)
        {
            Groups.Add(group);
        }
    }


    public class ArmyConstructor : MonoBehaviour
    {
        public static ArmyConstructor Instance;

        public BasicEvent GroupAddedEvent;
        private int Maxsize;
        private ArmyGroup ArmyGroup;

        [SerializeField]
        private Transform DynamicContainer;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void Init(BattleSettings battleSettings)
        {
            ArmyGroup = new ArmyGroup();
            Maxsize = battleSettings.ArmymaxSize;
        }

        public void Addgroup(GroupData groupData, Transform transform)
        {
            if (CanCreateGroup())
            {
                Group group = new Group(groupData, transform, DynamicContainer);
                ArmyGroup.Add(group);
                GroupAddedEvent.Raise();
            }
        }

        public void AddObject(ClickObject clickObject, Transform transform)
        {
            ClickObject obj = Instantiate(clickObject, transform.position , transform.rotation, DynamicContainer);
            ArmyGroup.Add(obj);
           // GroupAddedEvent.Raise();
        }

        private bool CanCreateGroup() { return (ArmyGroup.GetGroups().Count < Maxsize); }

        public List<Group> Groups() { return ArmyGroup.GetGroups(); }

        public List<ClickObject> Objects()
        {
            return ArmyGroup.GetObjects();
        }



    }
}
