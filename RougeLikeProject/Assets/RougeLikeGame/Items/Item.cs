using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        [SerializeField]
        public ItemSettings Settings;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
            _spriteRenderer.sprite = Settings.Sprite;

            var spriteSize = _spriteRenderer.bounds.size;
            _collider.size = spriteSize;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Settings.Apply(other.gameObject);

            Destroy(gameObject);
        }
    }
}
