using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public enum WeaponBehaviourState
    {
        Searching,
        Active,
        Inactive
    }

    public class WeaponBehaviour : MonoBehaviour
    {
        public WeaponComponent WeaponComponent;
        public float SightRange;
        public float Accuracy;
        private WeaponBehaviourState WeaponBehaviourState;

        private ClickObject Target;

        private void Awake()
        {
            WeaponBehaviourState = WeaponBehaviourState.Searching;
        }

        public void ChangeState(WeaponBehaviourState behaviourState)
        {
            WeaponBehaviourState = behaviourState;
            switch (WeaponBehaviourState)
            {
                case WeaponBehaviourState.Searching:
                    Target = null;
                    break;
                case WeaponBehaviourState.Active:


                    break;
                case WeaponBehaviourState.Inactive:
                    break;
                default:
                    break;
            }
        }


        private void HandleBehaviour()
        {
            switch (WeaponBehaviourState)
            {
                case WeaponBehaviourState.Searching:
                    Target = FindTarget(ArmyConstructor.Instance.Objects());
              
                    break;
                case WeaponBehaviourState.Active:
                    if (Target && WeaponComponent.CanUseWeapon(Target))
                        WeaponComponent.UseWeapon(Target);
                    break;

            }

        }


        private ClickObject FindTarget(List<ClickObject> clickObjects)
        {
            ClickObject found = null;
            float distance = SightRange;
            foreach (ClickObject cobj in clickObjects)
            {
                float objDistance = Vector3.Distance(cobj.gameObject.transform.position, this.gameObject.transform.position);
                if (!cobj.Friendly && objDistance <= distance)
                {
                    found = cobj;
                    distance = objDistance;
                }
            }
            return found;
        }

        public void OnTargetDeath() { }

        public void OnAttackOrder(ClickObject clickObject) { }

        private void Update()
        {
            HandleBehaviour();
        }
    }
}