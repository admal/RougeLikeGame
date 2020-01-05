using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New weapon", menuName = "Weapons/New weapon")]
    public class WeaponData : ScriptableObject
    {
        public Sprite Sprite;
        public float Range;
        public string WeaponName;
        public float ParticleBaseSpeed; //???
        public float ShootFrequency;
        public GameObject ParticlePrefab;
    }
}
