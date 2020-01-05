using System.Collections;
using System.Collections.Generic;
using Leveling;
using UnityEngine;


namespace Items.ItemTypes
{
    [CreateAssetMenu(fileName = "Experience", menuName = "Items/Consumables/Experience")]
    public class ExperienceItemType : ItemSettings
    {
        public int ExperienceAmount;
        public override void Apply(GameObject gameObject)
        {
            var experienceController = gameObject.GetComponent<ExperienceController>();
            if (experienceController != null)
            {
                experienceController.AddExperience(ExperienceAmount);
            }
        }
    }
}