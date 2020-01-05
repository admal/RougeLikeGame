using System.Collections;
using System.Collections.Generic;
using Common;
using MazeGeneration;
using UnityEngine;

namespace Doors
{
    public class Door : MonoBehaviour, IDoor
    {
        [SerializeField]
        private MazeRoomNeighbourPosition _to;
        public MazeRoomNeighbourPosition To => _to;
        private DoorController _controller;

        [SerializeField]
        private Transform _spawnPoint;
        public Vector3 SpawnPoint => _spawnPoint.position;
        private bool _isOpen;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        [SerializeField]
        private Sprite _openedDoorSprite;

        [SerializeField]
        private Sprite _closedDoorSprite;

        private void Awake()
        {
            _controller = new DoorController(this);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.IsPlayer() && _isOpen)
            {
                MazeGeneratorObject.Instance.MoveToRoom(_to);
            }
        }

        public void Close()
        {
            _isOpen = false;
            _spriteRenderer.sprite = _closedDoorSprite;
            _collider.enabled = false;
        }

        public void Open()
        {
            _isOpen = true;
            _spriteRenderer.sprite = _openedDoorSprite;
            _collider.enabled = true;
        }
    }
}