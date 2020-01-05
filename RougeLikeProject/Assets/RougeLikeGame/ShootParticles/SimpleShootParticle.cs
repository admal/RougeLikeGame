using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Damage;

namespace ShootParticles
{
    //TODO: make base class for it
    public class SimpleShootParticle : MonoBehaviour
    {
        // private Vector2 ShootDirection;
        private float Range;
        // // private float Velocity;

        private bool IsShot = false;

        private Vector2 _spawnPosition;

        private Rigidbody2D _rigidBody;
        private GameTag _from;
        void Awake()
        {
            _spawnPosition = transform.position;
            _rigidBody = GetComponent<Rigidbody2D>();

        }

        void Update()
        {
            if (Vector2.Distance(_spawnPosition, transform.position) > Range)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.HasTag(_from))
            {
                return;
            }
            var damagable = other.gameObject.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.ApplyDamage(new DamageInformation() { BaseDamage = 1 });
            }
            Destroy(gameObject);
        }

        public void ShootParticle(Vector2 shootDirection, float range, float speed, GameTag from)
        {
            _from = from;
            IsShot = true;
            // ShootDirection = shootDirection;
            Range = range;

            _rigidBody.velocity = shootDirection * speed;
        }
    }
}
