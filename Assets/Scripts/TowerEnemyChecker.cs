using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyChecker : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    public List<EnemyBase> detectedEnemies { get; private set; }

    [field: SerializeField] public float range { get; private set; }           // enum (scriptable object) for short/medium/long?

    // Start is called before the first frame update
    void Start()
    {
        detectedEnemies = new List<EnemyBase>();
    }

    public void CheckForEnemiesInRange()
    {
        detectedEnemies.Clear();

        // top foreach does not work. apparently basing it on a ScriptableObject list does not allow for the proper references
        //foreach (EnemyBase enemy in enemyManager.GetCurrentWave().enemies)
        foreach (EnemyBase enemy in enemyManager.GetComponentsInChildren<EnemyBase>())
        {
            float dist = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (dist <= range)
            {
                detectedEnemies.Add(enemy);
            }
        }
    }
}
