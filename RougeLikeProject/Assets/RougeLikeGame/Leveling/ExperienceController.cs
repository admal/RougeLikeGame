using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leveling
{
    public class ExperienceController : MonoBehaviour
    {
        public int CurrentExperience { get; private set; } = 0;

        public event Action<int, int> OnExperienceEarned = delegate { };

        public void AddExperience(int experience)
        {
            CurrentExperience += experience;
            OnExperienceEarned(experience, CurrentExperience);
        }

        public void ResetExperience()
        {
            CurrentExperience = 0;
        }
    }
}