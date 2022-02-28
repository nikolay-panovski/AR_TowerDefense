using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyChecker : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    public List<EnemyBase> detectedEnemies { get; private set; }

    [SerializeField] private float range;           // enum (scriptable object) for short/medium/long?

    // Start is called before the first frame update
    void Start()
    {
        detectedEnemies = new List<EnemyBase>();
    }

    public void CheckForEnemiesInRange()
    {
        detectedEnemies.Clear();

        foreach (EnemyBase enemy in enemyManager.GetCurrentWave().enemies)
        {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) <= range)
            {
                detectedEnemies.Add(enemy);
            }
        }
    }
}
