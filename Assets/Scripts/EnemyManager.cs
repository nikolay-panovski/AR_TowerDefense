using System.Collections.Generic;
using UnityEngine;

// Hold Lists of enemies, modifiable outside in Editor
public class EnemyManager : MonoBehaviour
{
    // might need to send isWaveActive back to gameManager for consistency
    [SerializeField] private GameManager gameManager;

    [SerializeField] private FloatValue timeBetweenWaves;
    private float currentTimeBetweenWaves;
    [SerializeField] private FloatValue timeBetweenEnemies;
    private float currentSpawnCooldown;
    public BoolValue isWaveActive;

    public List<EnemyWave> enemyWaves;
    private EnemyWave currentWave = null;
    private EnemyBase currentEnemy = null;
    //private int currentWaveIndex;
    private int currentEnemyIndex;

    private bool canSpawn = true;

    void Start()
    {
        currentWave = enemyWaves[0];
        currentEnemy = currentWave.enemies[0];
        currentEnemyIndex = 0;
    }

    void Update()
    {
        // step 1: wave gets activated via timer
        // TODO: hook timer to UI and display it for information
        if (isWaveActive.value == false)
        {
            isWaveActive.value = incrementAndCheckCooldown(ref currentTimeBetweenWaves, timeBetweenWaves.value);
        }

        // step 2: now that wave is active, start spawning on timer, until there are no enemies left
        if (isWaveActive.value == true)
        {
            if (canSpawn)
            {
                spawnEnemy(currentEnemy);

                currentEnemyIndex++;
                if (currentEnemyIndex >= currentWave.enemies.Count)
                {
                    // initiate constant checking whether all enemies' GameObjects are destroyed
                    // THIS LOOP DOES FREEZE GAME! (especially when nothing is able to kill the enemies)
                    /**
                    while (true)
                    {
                        EnemyBase remainingEnemy = null;

                        foreach (EnemyBase enemy in currentWave.enemies)
                        {
                            if (enemy != null)
                            {
                                remainingEnemy = enemy;
                                break;
                            }
                        }

                        if (remainingEnemy == null) break;
                        //else Debug.Log("Still enemies left alive!");
                    }
                    /**/

                    // ~step 3: deactivate wave, prepare next wave for active, return to step 1
                    isWaveActive.value = false;
                    Debug.Log("All enemies for this wave spawned, now wave is inactive!");

                    // ~~waves should all be unique, but if they are not, use the other int for an index
                    int nextWaveIndex = enemyWaves.IndexOf(currentWave) + 1;
                    if (nextWaveIndex >= enemyWaves.Count)
                    {
                        // final: all waves cleared
                        Debug.Log("All enemies were spawned and cleared, Game Win!");
                    }
                    else
                    {
                        currentWave = enemyWaves[nextWaveIndex];
                    }
                }
                else
                {
                    currentEnemy = currentWave.enemies[currentEnemyIndex];
                }
            }

            canSpawn = incrementAndCheckCooldown(ref currentSpawnCooldown, timeBetweenEnemies.value);
        }
    }

    public EnemyWave GetCurrentWave()
    {
        return currentWave;
    }

    private bool incrementAndCheckCooldown(ref float cooldown, float maxCooldown)
    {
        cooldown += Time.deltaTime;

        if (cooldown >= maxCooldown)
        {
            cooldown = 0.0f;
            return true;
        }

        return false;
    }

    private void spawnEnemy(EnemyBase enemy)
    {
        GameObject.Instantiate(enemy, this.transform);
    }
}
