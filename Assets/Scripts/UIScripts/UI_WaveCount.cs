using UnityEngine;

public class UI_WaveCount : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyManager enemyManager;
    private TMPro.TMP_Text countText;
    private int maxWaves;

    private void Start()
    {
        countText = GetComponent<TMPro.TMP_Text>();

        maxWaves = enemyManager.enemyWaves.Count;
    }

    private void FixedUpdate()
    {
        countText.text = (enemyManager.enemyWaves.IndexOf(enemyManager.currentWave) + 1) + "/" + maxWaves;
    }
}