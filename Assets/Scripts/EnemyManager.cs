using System.Collections.Generic;
using UnityEngine;

// Hold Lists of enemies, modifiable outside in Editor
public class EnemyManager : MonoBehaviour
{
    // sent isWaveActive back to gameManager for consistency
    [SerializeField] private GameManager gameManager;

    
    public FloatValue currentTimeBetweenWaves;
    [SerializeField] private FloatValue timeBetweenEnemies;
    private float currentSpawnCooldown;
    

    public List<EnemyWave> enemyWaves;
    public EnemyWave currentWave { get; private set; } = null;
    private EnemyBase currentEnemy = null;
    //private int currentWaveIndex;
    private int currentEnemyIndex;

    private bool canSpawn = false;

    void Start()
    {
        currentWave = enemyWaves[0];
        currentEnemy = currentWave.enemies[0];
        currentEnemyIndex = 0;
    }

    void Update()
    {
        if (gameManager.isGameValid.value == true)
        {
            if (gameManager.playerHP.value <= 0)
            {
                gameManager.isGameBeaten.value = false;
                gameManager.isGameValid.value = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene("UIGameOver");
            }

            // step 1: wave gets activated via timer
            if (gameManager.isGameBeaten.value == false && gameManager.isWaveActive.value == false)
            {
                gameManager.isWaveActive.value = incrementAndCheckCooldown(ref currentTimeBetweenWaves.value, gameManager.timeBetweenWaves.value);
            }

            // step 2: now that wave is active, start spawning on timer, until there are no enemies left
            if (gameManager.isWaveActive.value == true)
            {
                if (canSpawn)
                {
                    if (currentEnemyIndex < currentWave.enemies.Count)
                    {
                        spawnEnemy(currentEnemy);

                        currentEnemyIndex++;
                    }

                    if (currentEnemyIndex >= currentWave.enemies.Count)
                    {
                        // initiate constant checking whether all enemies' GameObjects are destroyed
                        // might become heavy... but it works...
                        foreach (EnemyBase enemy in GetComponentsInChildren<EnemyBase>())
                        {
                            if (enemy != null)
                            {
                                return;
                            }
                        }

                        // ~step 3: all enemies destroyed, deactivate wave, prepare next wave for active, return to step 1
                        gameManager.isWaveActive.value = false;
                        currentEnemyIndex = 0;

                        int nextWaveIndex = enemyWaves.IndexOf(currentWave) + 1;
                        if (nextWaveIndex >= enemyWaves.Count)
                        {
                            gameManager.isGameBeaten.value = true;
                            gameManager.isGameValid.value = false;
                            UnityEngine.SceneManagement.SceneManager.LoadScene("UIWin");
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
