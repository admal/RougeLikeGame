using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.EnemyLogic
{
    public class ChargingEnemyController
    {
        private IChargingEnemy _enemy;
        
        public ChargingEnemyController(IChargingEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}