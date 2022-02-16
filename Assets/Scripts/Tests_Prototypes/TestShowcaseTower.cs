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
    private GameObject firstEnemyInRange = null;

    [Tooltip("Set to true to print distance between this tower and an enemy to the console. It will be printed every frame!")]
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
            firstEnemyInRange = checkForEnemiesInRange();

            if (firstEnemyInRange != null)
            {
                bulletCooldown += Time.deltaTime;

                if (bulletCooldown >= shootCooldown)
                {
                    if (bulletPrefab != null)
                    {
                        GameObject newBullet = Instantiate(bulletPrefab);
                        newBullet.transform.parent = transform.parent /*this.transform*/;
                        TestBullet script = newBullet.GetComponent<TestBullet>();
                        script.dmg = this.damage;
                        script.followEnemy = firstEnemyInRange;
                        //newBullet.transform.localScale *= 0.04f;      // for "guidance" in case rescaling is needed
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

    private GameObject checkForEnemiesInRange()
    {
        /**/
        GameObject enemy = GameObject.FindWithTag("Enemy");

        if (enemy == null)
        {
            return null;
        }
        else
        {
            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (printDistance)
            {
                Debug.Log("Distance between tower and enemy: " + distance + " (tower range is: " + range + ")");
            }

            if (distance < range)
            {
                return enemy;
            }
        }

        return null;
        /**/

        /**
        foreach (GameObject enemy in TestEnemyManager.enemyList)
        {
            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (printDistance)
            {
                Debug.Log("Distance between tower and enemy: " + distance + " (tower range is: " + range + ")");
            }

            if (distance < range)
            {
                return enemy;
            }
        }
        return null;
        /**/
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
