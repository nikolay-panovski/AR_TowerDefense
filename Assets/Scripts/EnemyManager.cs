using System.Collections.Generic;
using UnityEngine;

// Hold Lists of enemies, modifiable outside in Editor
public class EnemyManager : MonoBehaviour
{
    public List<EnemyWave> enemyWaves;
    private EnemyWave currentWave = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EnemyWave GetCurrentWave()
    {
        return currentWave;
    }
}
