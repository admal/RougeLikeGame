using System.Collections;
using System.Collections.Generic;
using Items.ItemTypes;
using UnityEngine;

namespace Items
{
    public abstract class ItemSettings : ScriptableObject
    {
        public string ItemName;
        public string ItemDescription;
        public Sprite Sprite;
        public abstract void Apply(GameObject gameObject);
    }
}