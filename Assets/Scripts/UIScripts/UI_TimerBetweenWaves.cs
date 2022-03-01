using UnityEngine;

public class UI_TimerBetweenWaves : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemyManager enemyManager;
    private TMPro.TMP_Text timerText;

    private void Start()
    {
        timerText = GetComponent<TMPro.TMP_Text>();
    }

    private void FixedUpdate()
    {
        if (gameManager.isWaveActive.value == true || gameManager.isGameBeaten.value == true)
        {
            timerText.text = "";
        }

        else
        {
            timerText.text = (gameManager.timeBetweenWaves.value - (int)enemyManager.currentTimeBetweenWaves.value).ToString();
        }
    }
}
