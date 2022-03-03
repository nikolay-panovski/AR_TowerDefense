using UnityEngine;

public class RescaleRangeCircle : MonoBehaviour
{
    private TowerEnemyChecker enemyChecker;

    void Start()
    {
        enemyChecker = GetComponentInParent<TowerEnemyChecker>();
        // do not even ask me why 12.5 is a sensible scale (previously it was 150)
        gameObject.transform.localScale = new Vector3(12.5f * enemyChecker.range, 12.5f * enemyChecker.range, 1);
    }
}
