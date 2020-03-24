using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FUBAR
{

    public abstract class GroupOperation : ScriptableObject
    {
        public abstract bool CanUseOperation(Group group);
        public abstract void UseOperation(Group group);
    }
}