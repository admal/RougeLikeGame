using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.ItemTypes
{
    [CreateAssetMenu(fileName = "HP up", menuName = "Items/Consumables/Increase health")]
    public class IncreaseHealthEffectSettings : ItemSettings
    {
        public int IncreaseHealthValue;

        public override void Apply(GameObject gameObject)
        {
            var health = gameObject.GetComponent<Health.Health>();
            if (health != null)
            {
                health.ChangeHealth(IncreaseHealthValue);                
            }
        }
    }
}