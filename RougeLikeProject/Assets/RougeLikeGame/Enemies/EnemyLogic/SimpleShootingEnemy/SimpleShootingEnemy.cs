using System.Collections;
using System.Collections.Generic;
using Common;
using Player;
using ShootParticles;
using UnityEngine;
using Weapons;

namespace Enemies.EnemyLogic
{
    public interface ISimpleShootingEnemy
    {
        Vector3 PlayerPosition { get; }
        bool IsPlayerVisible { get; }
        SimpleShootingEnemySettings Settings { get; }
        void StartShooting(Vector2 position);
        void StopShooting();
    }

    public class SimpleShootingEnemy : EnemyBase, ISimpleShootingEnemy
    {
        private SimpleShootingEnemyController _controller;

        [SerializeField]
        private GameObject _shootParticle;

        [SerializeField]
        private SimpleShootingEnemySettings _settings;
        public SimpleShootingEnemySettings Settings => _settings;

        public Vector3 PlayerPosition { get; private set; }
        public bool IsPlayerVisible { get; private set; }
        private Weapon _weapon;

        void Awake()
        {
            _controller = new SimpleShootingEnemyController(this);
            _weapon = GetComponent<Weapon>();
        }

        void Update()
        {
            var playerPosition = PlayerManager.Instance.Position;

            var inRange = Vector2.Distance(playerPosition, transform.position) <= _settings.ShootingRange;
            if (inRange)
            {
                Debug.DrawRay(transform.position, (playerPosition - transform.position), Color.green, 0.1f);

            }
            else
            {
                Debug.DrawRay(transform.position, (playerPosition - transform.position), Color.red, 0.1f);
            }

            var layerMask = GameLayer.GetMask(GameLayers.RoomBoundaries, GameLayers.LandObstacles, GameLayers.FlyingPlayer, GameLayers.Player);

            var hit = Physics2D.Raycast(transform.position, (playerPosition - transform.position), 100, layerMask);
            if (inRange && hit.transform.gameObject.IsPlayer())
            {
                IsPlayerVisible = true;
                PlayerPosition = playerPosition;
            }
            else
            {
                IsPlayerVisible = false;
            }

            _controller.TryShoot();
        }

        public void StartShooting(Vector2 position)
        {
            var direction = position - transform.position.ToVector2();
            direction.Normalize();
            _weapon.StartShooting(direction);
        }

        public void StopShooting()
        {
            _weapon.StopShooting();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _settings.ShootingRange);   
        }

    }
}