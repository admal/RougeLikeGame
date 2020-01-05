using System.Collections;
using System.Collections.Generic;
using Common;
using GameManagers;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
        private bool _isOpen;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        [SerializeField]
        private Sprite _openedDoorSprite;

        [SerializeField]
        private Sprite _closedDoorSprite;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            Open(); //TODO: VERY TMP!!!
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.IsPlayer() && _isOpen)
            {
                Debug.Log("Enter new room");
                LevelManager.Instance.GotoNextLevel();
            }
        }

        public void Close()
        {
            _isOpen = false;
            // _spriteRenderer.sprite = _closedDoorSprite;
            _collider.enabled = false;
        }

        public void Open()
        {
            _isOpen = true;
            _spriteRenderer.sprite = _openedDoorSprite;
            _collider.enabled = true;
        }
}
