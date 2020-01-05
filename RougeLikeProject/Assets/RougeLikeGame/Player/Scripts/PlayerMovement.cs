using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using MazeGeneration;
using ShootParticles;
using UnityEngine;
using Weapons;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IPlayerMovement
    {
        [SerializeField]
        private PlayerStatistics _playerStatistics;
        private bool _dodgeRequest = false;
        private float _dodgeStartTime;
        [SerializeField]
        private float _dodgeTimeLength;
        [SerializeField]
        private float _dodgeFrequency; //TODO: rename

        private bool _dodgeButtonPressed = false;

        private Rigidbody2D _rigidBody;
        private PlayerMovementController _controller;

        private Vector2 _velocity;
        private Weapon _weapon;
        private Collider2D _collider;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _weapon = GetComponent<Weapon>();
            _collider = GetComponent<Collider2D>();

            //TODO: TMP!!!! Move it when main menu is moved
            GameManagers.GameManager.Instance.LoadPresets();
            GameManagers.GameManager.Instance.InitHash();
        }

        private void Update()
        {
            if (_dodgeRequest == false)
            {
                var h = Input.GetAxisRaw("Horizontal");
                var v = Input.GetAxisRaw("Vertical");
                _velocity = new Vector2(h, v);

                var shotH = Input.GetAxisRaw("FireHorizontal");
                var shotV = Input.GetAxisRaw("FireVertical");
                var shotDirection = new Vector2(shotH, shotV);
                if (shotDirection.x != 0 || shotDirection.y != 0)
                {
                   _weapon.SingleShoot(new Vector2(shotH, shotV));
                }                
            }


            if (Time.time > _dodgeStartTime + _dodgeTimeLength)
            {
                _dodgeRequest = false;
                PlayerManager.Instance.PlayerHealth.StopImmunity();
                _collider.enabled = true;
            }

            if (Input.GetButton("Jump") && Time.time > _dodgeStartTime + _dodgeTimeLength + _dodgeFrequency && _dodgeButtonPressed == false)
            {
                _dodgeRequest = true;
                _dodgeStartTime = Time.time;
                PlayerManager.Instance.PlayerHealth.StartImmunity();
                _collider.enabled = false;
                
                _dodgeButtonPressed = true;
            }

            if (Input.GetButton("Jump") == false && _dodgeButtonPressed)
            {
                _dodgeButtonPressed = false;
            }

        }

        private void FixedUpdate()
        {
            var speed = _dodgeRequest ? _playerStatistics.DodgeSpeed : _playerStatistics.MovementSpeed;

            _rigidBody.velocity = _velocity * speed;
        }
    }
}