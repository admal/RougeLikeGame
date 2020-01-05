using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Health;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.HealthTests
{
    public class HealthControllerTests
    {
        private const int MaxHealth = 6;
        private const int InitialHealth = 5;
        private HealthController _healthController;
        private IHealth _health;

        public void Init()
        {
            _health = new HealthMock(MaxHealth, InitialHealth);
            _healthController = new HealthController(_health);
        }

        [Test]
        public void ChangeHealth()
        {
            //arrange
            Init();

            //act
            _healthController.ChangeHealth(-2);

            //assert
            Assert.AreEqual(InitialHealth - 2, _health.CurrentHealth);
        }

        [Test]
        [TestCase(-10, 0)]
        [TestCase(10, MaxHealth)]
        public void ChangeHealthOverLimits(int amount, int expectedResult)
        {
            //arrange
            Init();

            //act
            _healthController.ChangeHealth(amount);

            //assert
            Assert.AreEqual(expectedResult, _health.CurrentHealth);
        }

        [Test]
        public void Kill()
        {
            //arrange
            Init();
            // var wasKilled = false;
            // _healthController.OnDeath += () => wasKilled = true;

            //act
            _healthController.ChangeHealth(-_health.CurrentHealth);
            Thread.Sleep(500);
            //assert
            Assert.AreEqual(0, _health.CurrentHealth);
            // Assert.IsTrue(wasKilled);
        }

        [Test]
        [TestCase(-2, InitialHealth)]
        [TestCase(1, InitialHealth + 1)]
        public void ChangeHealthWhenImmune(int amount, int expectedResult)
        {
            //arrange
            Init();
            _health.IsImmune = true;

            //act
            _healthController.ChangeHealth(amount);

            //assert
            Assert.AreEqual(expectedResult, _health.CurrentHealth);
        }
    }
}
