using UnityEngine;
using UnityEngine.UI;

public class EnemyUIHPBar : MonoBehaviour
{
    private EnemyBase enemyBase;
    private Slider slider;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        slider = GetComponentInChildren<Slider>();
    }

    void FixedUpdate()
    {
        slider.value = enemyBase.hp / enemyBase.maxHP;
    }
}
