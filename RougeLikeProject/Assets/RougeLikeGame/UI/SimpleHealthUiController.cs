using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class SimpleHealthUiController : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            _text.text = PlayerManager.Instance.Health.CurrentHealth.ToString();
            PlayerManager.Instance.Health.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            var health = PlayerManager.Instance.Health.CurrentHealth;
            _text.text = health.ToString();
        }

        private void OnDestroy()
        {
            PlayerManager.Instance.Health.OnHealthChanged -= OnHealthChanged;
        }
    }
}