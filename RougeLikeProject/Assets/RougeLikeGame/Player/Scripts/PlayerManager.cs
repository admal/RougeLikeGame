using System.Collections;
using System.Collections.Generic;
using Health;
using UnityEngine;



namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        public Health.Health Health { get; private set; }
        public PlayerHealth PlayerHealth { get; private set; }
        public Vector3 Position { get => transform.position; set => transform.position = value; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There exists another instance of Player manager!");
            }
            else
            {
                Instance = this;
                Health = GetComponent<Health.Health>();
                PlayerHealth = GetComponent<PlayerHealth>();
            }
        }
    }
}