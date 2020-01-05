using System.Collections;
using System.Collections.Generic;
using Common;
using ShootParticles;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private WeaponData _weaponData;

        private float _lastShootTime;

        private bool _continuousFire = false;
        private Vector2 _continuousFireDirection;

        void Start()
        {

        }

        void Update()
        {
            if(_continuousFire)
            {
                SingleShoot(_continuousFireDirection);
            }
        }

        public void SingleShoot(Vector2 direction)
        {
            if(_lastShootTime + _weaponData.ShootFrequency > Time.time)
            {
                return;
            }
            _lastShootTime = Time.time;
            var particle = Instantiate(_weaponData.ParticlePrefab, transform.position, Quaternion.FromToRotation(Vector2.up, direction));
            //TODO: get base class of shoot particle
            var particleController = particle.GetComponent<SimpleShootParticle>();
            particleController.ShootParticle(direction, _weaponData.Range, _weaponData.ParticleBaseSpeed, gameObject.tag.ToGameTag());
        }

        public void StartShooting(Vector2 direction)
        {
            _continuousFire = true;
            _continuousFireDirection = direction;
        }

        public void StopShooting()
        {
            _continuousFire = false;
            _continuousFireDirection = new Vector2();
        }
    }
}
