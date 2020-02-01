using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUBAR
{
    public class Health : MonoBehaviour
    {
        public GameObjectUnityEvent OnDeathEvent;

        [SerializeField]
        private int MaxHealth;

        private int CurrentHealth;

        public void Heal() { }

        public void TakeDamage() { }

        public void OnDeath()
        {
            OnDeathEvent.Invoke(this.gameObject);
        }
    }
}