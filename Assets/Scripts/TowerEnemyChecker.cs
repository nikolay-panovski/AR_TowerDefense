using UnityEngine;

public interface IEnemyChecker
{
    public void CheckForEnemiesInRange();
}

public class TowerEnemyChecker : MonoBehaviour, IEnemyChecker
{
    private EnemyBase detectedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForEnemiesInRange()
    {

    }
}