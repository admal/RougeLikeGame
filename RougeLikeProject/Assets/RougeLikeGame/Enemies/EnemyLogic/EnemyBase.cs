using System.Collections;
using System.Collections.Generic;
using Enemies.Spawning;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private List<EnemyType> _types;
    public List<EnemyType> Types => _types;
}
