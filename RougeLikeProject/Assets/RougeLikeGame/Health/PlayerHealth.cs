using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private float _immunityTimeAfterHit;
        private Health _health;
        public event Action OnPlayerDeath = delegate {};
        private void Start()
        {
            _health = GetComponent<Health>();
            _health.OnHealthChanged += OnHealthChanged;
            _health.OnDeath += OnPlayerDeath;
        }

        private void OnHealthChanged()
        {
            if (_health.IsImmune == false)
            {
                StartCoroutine(MakePlayerImmune());
            }
        }

        public void StartImmunity()
        {
            _health.IsImmune = true;
        }

        public void StopImmunity()
        {
            _health.IsImmune = false;
        }

        private IEnumerator MakePlayerImmune()
        {
            StartImmunity();
            yield return new WaitForSeconds(_immunityTimeAfterHit);
            StopImmunity();
        }
    }
}
