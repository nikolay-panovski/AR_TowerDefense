using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TestEnemyManager : MonoBehaviour
{
    private ImageTargetBehaviour parentImageTarget;
    private bool isActive = false;

    // ensure there is only ONE enemy manager in the scene, and it spawns enemies!
    // (this is in order to not pass manager reference to towers, for simplicity)
    // in which the objects should have TestShowcaseEnemy script
    public static List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] private GameObject enemyPrefab;
    [Header("Seconds for this object to wait before spawning an enemy.")]
    [SerializeField] private float spawnCooldown = 2.0f;
    private float lastEnemyAt = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        parentImageTarget = transform.parent.GetComponent<ImageTargetBehaviour>();
        parentImageTarget.OnTargetStatusChanged += UpdateStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Time.time - lastEnemyAt >= spawnCooldown)
            {
                if (enemyPrefab != null)
                {
                    GameObject newEnemy = Instantiate(enemyPrefab);
                    enemyList.Add(newEnemy);
                }
                else
                {
                    Debug.LogError("Enemy Prefab is empty, assign one to spawn enemies!");
                }

                lastEnemyAt = Time.time;
            }
        }
    }

    public void UpdateStatus(ObserverBehaviour ob, TargetStatus status)
    {
        if ((status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED) && status.StatusInfo == StatusInfo.NORMAL)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    private void OnDestroy()
    {
        parentImageTarget.OnTargetStatusChanged -= UpdateStatus;
    }
}
