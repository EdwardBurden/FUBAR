using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR {
    public abstract class SelectionState
    {
        public abstract void Move(MoveOrder order);

        public abstract void Attack(AttackOrder order);

        public abstract void Attach(AttachOrder order);

        public abstract void Init();
        public abstract void StateLost();
    }
}