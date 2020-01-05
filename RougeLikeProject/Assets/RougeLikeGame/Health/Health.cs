using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Health
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private BaseStatistics _statistics;
        private HealthController _healthController;

        public event Action OnDeath = delegate{};
        public event Action OnHealthChanged = delegate{};

        public int CurrentHealth { get; set; }

        public int MaxHealth {get; private set; }

        public bool IsImmune { get; set; }

        private void Awake()
        {
            _healthController = new HealthController(this);
            MaxHealth = _statistics.MaxHealth;
            CurrentHealth = _statistics.MaxHealth;
        }

        public void ChangeHealth(int amount)
        {
            var healthChanged = _healthController.ChangeHealth(amount);
            if (healthChanged)
            {
                OnHealthChanged();
            }
        }

        void IHealth.Kill()
        {
            OnDeath();
        }
    }
}