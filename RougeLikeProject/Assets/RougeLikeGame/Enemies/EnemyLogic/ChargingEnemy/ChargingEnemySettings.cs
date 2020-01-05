using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charging enemy statistics", menuName = "Statistics/Charging enemy statistics")]
public class ChargingEnemySettings : BaseStatistics
{
    public float FollowRange;
    public bool AlwaysSeePlayer = false;
}
