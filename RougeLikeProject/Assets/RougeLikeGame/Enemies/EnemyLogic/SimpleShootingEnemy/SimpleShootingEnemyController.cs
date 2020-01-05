using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.EnemyLogic
{
    public class SimpleShootingEnemyController
    {
        private ISimpleShootingEnemy _simpleShootingEnemy;
        public SimpleShootingEnemyController(ISimpleShootingEnemy simpleShootingEnemy)
        {
            _simpleShootingEnemy = simpleShootingEnemy;
        }

        public void TryShoot()
        {
            var canShoot = _simpleShootingEnemy.IsPlayerVisible;
            if (canShoot)
            {
                var shootPosition = new Vector2(_simpleShootingEnemy.PlayerPosition.x, _simpleShootingEnemy.PlayerPosition.y);                
                _simpleShootingEnemy.StartShooting(shootPosition);
            }
            else
            {
                _simpleShootingEnemy.StopShooting();
            }
        }
    }
}