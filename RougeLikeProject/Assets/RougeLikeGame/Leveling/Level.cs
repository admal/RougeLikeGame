using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leveling
{
    public class Level : MonoBehaviour
    {
        /// <summary>
        /// Level can be set in editor - it is caused that sometimes we want to have object with fixed level
        /// For example on spawn set enemy level determined by the difficulty
        /// </summary>
        public int CurrentLevel = 0;

        [SerializeField]
        private List<int> _experienceLevels;
        private ExperienceController _experienceController;

        void Start()
        {
            _experienceController = GetComponent<ExperienceController>();
            if (_experienceController != null)
            {
                _experienceController.OnExperienceEarned += OnExperienceEarned;
            }
        }

        private void OnExperienceEarned(int amount, int currentExperience)
        {
            var nextLevel = CurrentLevel + 1;
            if ( nextLevel <= _experienceLevels.Count && currentExperience >= _experienceLevels[CurrentLevel])
            {
                CurrentLevel++;
                _experienceController.ResetExperience();
            }
        }
    }
}