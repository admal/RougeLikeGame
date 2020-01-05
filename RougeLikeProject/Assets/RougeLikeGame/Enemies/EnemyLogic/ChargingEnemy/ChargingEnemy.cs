using System.Collections;
using System.Collections.Generic;
using Common;
using Player;
using UnityEngine;
using Damage;

namespace Enemies.EnemyLogic
{
    public interface IChargingEnemy
    {
        ChargingEnemySettings Settings { get; }
        Vector3 PlayerPosition { get; }
        bool IsPlayerVisible { get; }
    }

    public class ChargingEnemy : EnemyBase, IChargingEnemy
    {
        private ChargingEnemyController _controller;
        [SerializeField]
        private ChargingEnemySettings _settings;
        public ChargingEnemySettings Settings => _settings;
        public Vector3 PlayerPosition { get; private set; }

        public bool IsPlayerVisible { get; private set; }

        private void Awake()
        {
            _controller = new ChargingEnemyController(this);
        }

        private void Update()
        {
            var rayLength = 1f;
            var playerPosition = PlayerManager.Instance.Position;

            if (IsPlayerVisible)
            {
                Debug.DrawRay(transform.position, (playerPosition - transform.position) * rayLength, Color.green, 0.1f);

            }
            else
            {
                Debug.DrawRay(transform.position, (playerPosition - transform.position) * rayLength, Color.red, 0.1f);
            }

            var layerMask = GameLayer.GetMask(GameLayers.RoomBoundaries, GameLayers.LandObstacles, GameLayers.FlyingPlayer, GameLayers.Player);
            var hit = Physics2D.Raycast(transform.position, (playerPosition - transform.position), _settings.FollowRange, layerMask);
            if (hit.transform.gameObject.IsPlayer() || _settings.AlwaysSeePlayer)
            {
                IsPlayerVisible = true;
                PlayerPosition = playerPosition;
            }
            else
            {
                IsPlayerVisible = false;
            }

            FollowPlayer();
        }

        public void FollowPlayer()
        {
            var distance = Vector2.Distance(transform.position, PlayerPosition);
            if (IsPlayerVisible && _settings.IsFlying)
            {
                var newPos = Vector2.MoveTowards(transform.position, PlayerPosition, _settings.MovementSpeed);
                transform.position = newPos;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsPlayer())
            {
                other.gameObject.GetComponent<Damagable>().ApplyDamage(new DamageInformation()
                {
                    BaseDamage = 2
                });
            }
        }
    }
}