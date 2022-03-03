using UnityEngine;

public class RescaleRangeCircle : MonoBehaviour
{
    private TowerEnemyChecker enemyChecker;

    void Start()
    {
        enemyChecker = GetComponentInParent<TowerEnemyChecker>();
        // do not even ask me why 150 is a sensible scale
        gameObject.transform.localScale = new Vector3(150 * enemyChecker.range, 150 * enemyChecker.range, 1);
    }
}
