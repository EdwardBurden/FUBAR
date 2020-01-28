using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR {
    public abstract class SelectionState
    {
        public abstract void Move(MoveOrder order);

        public abstract void Attack(AttackOrder order);

        public abstract void Attach();

        public abstract void Dettach();

        public abstract void Init();
        public abstract void StateLost();
    }
}