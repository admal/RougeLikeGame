using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public interface IPlayerMovement
    {

    }
    public class PlayerMovementController
    {
        private IPlayerMovement _player;
        public PlayerMovementController(IPlayerMovement playerMovement)
        {
            _player = playerMovement;
        }

        public void Move(Vector2 position)
        {

        }
    }
}