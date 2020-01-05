using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon statistics", menuName = "Statistics/Weapon statistics")]
public class WeaponStatistics : ScriptableObject
{
    public float BulletSpeed;
    public float Damage;
    public float MaxBulletFrequency;
    public GameObject Bullet;
}
