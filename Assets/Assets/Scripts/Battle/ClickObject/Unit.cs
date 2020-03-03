using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FUBAR
{
    [RequireComponent(typeof(WeaponBehaviour))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(MovementComponent))]
    public class Unit : ClickObject
    {
        public WeaponBehaviour WeaponBehaviour;
        public Health Health;
    }


}