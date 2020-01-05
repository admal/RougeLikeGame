using System.Collections;
using System.Collections.Generic;
using Health;
using UnityEngine;

namespace Tests.HealthTests
{
    public class HealthMock : IHealth
    {

        public int CurrentHealth { get; set; }

        public int MaxHealth { get; private set; }
        public bool IsImmune { get; set; }

        public HealthMock(int maxHealth, int currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public void Kill()
        {
        }
    }
}