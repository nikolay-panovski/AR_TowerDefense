using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Wave", menuName = "Custom Game Objects/Enemy Wave")]
public class EnemyWave : ScriptableObject
{
    public List<EnemyBase> enemies;
}