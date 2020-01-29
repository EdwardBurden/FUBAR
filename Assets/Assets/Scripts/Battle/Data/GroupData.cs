using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{
    [CreateAssetMenu(menuName = "New/GroupData")]
    public class GroupData : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public ClickObject Objects;
        public int ObjectNumber;
        public List<Operation> Operations;
        public ClickObject GroupFlag;
    }
}