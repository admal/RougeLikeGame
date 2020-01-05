using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.UI
{
    [RequireComponent(typeof(Health.Health))]
    public class EnemyDamageController : MonoBehaviour
    {
        private Health.Health _health;
        [SerializeField]
        private GameObject _healthLabel;

        //TMP!!!!
        [SerializeField]
        private GameObject _labelParent;
        // Start is called before the first frame update
        void Start()
        {
            _health = GetComponent<Health.Health>();
            _health.OnHealthChanged += OnHealthChanged;
            _health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }

        private void OnHealthChanged()
        {
            var label = Instantiate(_healthLabel, _labelParent.transform);
            Destroy(label, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}