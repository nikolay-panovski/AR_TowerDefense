using UnityEngine;
using Vuforia;

public class TestShowcaseTower : MonoBehaviour
{
    private ImageTargetBehaviour parentImageTarget;
    private bool isActive = false;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float shootCooldown = 2.0f;
    private float bulletCooldown = 0.0f;
    private bool enemyInRange = false;

    [Header("Set to true to print distance between this tower and an enemy to the console. It will be printed every frame!")]
    [SerializeField] private bool printDistance = false;

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
            enemyInRange = checkForEnemiesInRange();

            if (enemyInRange)
            {
                bulletCooldown += Time.deltaTime;

                if (bulletCooldown >= shootCooldown)
                {
                    if (bulletPrefab != null)
                    {
                        GameObject newBullet = Instantiate(bulletPrefab);
                        bulletPrefab.GetComponent<TestBullet>().dmg = this.damage;
                        //newBullet.transform.localScale *= 0.04f;      // for "guidance"
                    }
                    else
                    {
                        Debug.LogError("Bullet Prefab is empty, assign one to spawn bullets!");
                    }

                    bulletCooldown = 0.0f;
                }
            }
        } 
    }

    private bool checkForEnemiesInRange()
    {
        GameObject firstEnemy = TestEnemyManager.enemyList[0];

        if (firstEnemy != null)
        {
            float distance = Vector3.Distance(firstEnemy.transform.position, this.transform.position);
            if (printDistance)
            {
                Debug.Log("Distance between tower and first enemy: " + distance + " (tower range is: " + range + ")");
            }

            if (distance < range)
            {
                return true;
            }
        }

        return false;
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
