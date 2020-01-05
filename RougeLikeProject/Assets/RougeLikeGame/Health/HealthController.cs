using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; }
        bool IsImmune { get; set; }
        void Kill();
    }

    public class HealthController
    {
        private IHealth _health;
        public HealthController(IHealth health)
        {
            _health = health;
        }

        public bool ChangeHealth(int amount)
        {
            var isDamage = amount < 0;

            if (isDamage && _health.IsImmune)
            {
                return false;
            }

            _health.CurrentHealth += amount;

            if (_health.CurrentHealth <= 0)
            {
                _health.CurrentHealth = 0;
                _health.Kill();
            }
            else if (_health.CurrentHealth > _health.MaxHealth)
            {
                _health.CurrentHealth = _health.MaxHealth;
            }

            return true;
        }
    }

}
