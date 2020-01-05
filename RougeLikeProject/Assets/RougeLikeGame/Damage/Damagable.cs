using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    [RequireComponent(typeof(Health.Health))]
    public class Damagable : MonoBehaviour
    {
        private Health.Health _health;

        [SerializeField]
        private BaseStatistics _statistics;

        private void Start()
        {
            _health = GetComponent<Health.Health>();
        }

        public void ApplyDamage(DamageInformation damageInformation)
        {
            _health.ChangeHealth(-damageInformation.BaseDamage);
        }
    }
}
