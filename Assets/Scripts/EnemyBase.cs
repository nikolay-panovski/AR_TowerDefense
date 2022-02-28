using UnityEngine;

// Enemy that can be spawned from a start card,                                             -- EnemyManager/Spawner (not contained here)
// can walk forward with its typical speed from one waypoint passed to it to another,       -- EnemyMoveController (interface here, implement elsewhere)
// can die and update the player money on death,                                            -- this (HP check) + fire event on killed
// can reach goal, dying in the process and updating the player health on death.            -- this (interaction check) + fire event on goal/destroy

// Unity can go perform sudoku honestly
[RequireComponent(typeof(EnemyAutoMoveController))]
[RequireComponent(typeof(EnemyPathfindController))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;   // not the most optimal anymore ngl ... but good practice for SA

    [Header("Enemy stats")]
    [SerializeField] private float maxHP;
    private float hp;
    [SerializeField] private float initialSpeed;
    private float speed;
    [SerializeField] private int damageToGoal;
    [SerializeField] private int minScrapOnDeath;
    [SerializeField] private int maxScrapOnDeath;

    // !! Interfaces principle broken and classes are not assignable in Inspector, replaced all that with TryGetComponent !!
    // same in TowerBase.cs and wherever else relevant. format (of commented but listed interfaces) also preserved.
    private /*ITargetMoveController*/ EnemyAutoMoveController moveController;
    private /*IPathfindingController*/ EnemyPathfindController pathfindController;

    private bool hasReachedGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        speed = initialSpeed;

        bool gotMoveController, gotPathfController;

        gotMoveController = TryGetComponent<EnemyAutoMoveController>(out moveController);
        gotPathfController = TryGetComponent<EnemyPathfindController>(out pathfindController);

        if (!gotMoveController || !gotPathfController)
        {
            Debug.LogError("Enemy is missing a Move Controller script and/or a Pathfind Controller script! Errors will happen below!");
            //moveController.SetDestination(pathfindController.GetActiveWaypoint().GetPosition());      // activeWP starts as null
            // if activeWP is null at start and there are still no waypoints, then one gets added to [], set activeWP to that?
        }

        else
        {
            foreach (TestWaypoint wp in FindObjectsOfType<TestWaypoint>(false))
            {
                pathfindController.AddWaypointToList(wp);
            }
            pathfindController.SetFirstActiveWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            TestWaypoint activeWP = pathfindController.GetActiveTestWaypoint();
            if (activeWP != null)
            {
                moveController.SetDestination(activeWP.GetPosition());
                Vector3 distVec;
                moveController.MoveTowardsDestination(speed, out distVec);

                if (distVec.magnitude < gameManager.minDistToWaypoint.value)
                {
                    hasReachedGoal = !pathfindController.TestSetActiveWaypoint();
                    // **if the enemy path does not change on card move or seems broken, then we'd need to set the destination constantly in Update()
                    if (hasReachedGoal == false) moveController.SetDestination(pathfindController.GetActiveTestWaypoint().GetPosition());
                }
            }

            else
            {
                // ~~except that if this happens, we'd need an error message for the user who did not snapshot waypoints
                Debug.LogError("No active Waypoints were found. Did you get any rendered from the ImageTargets/cards?");
            }
        
        

        if (hasReachedGoal)
        {
            gameManager.ModifyPlayerValue(gameManager.playerHP, GameManager.Modification.DECREASE, damageToGoal);
            Debug.Log("An enemy has reached the goal. Goal HP will decrease.");
            Debug.Log("Player HP is now: " + gameManager.playerHP.value);
            Destroy(gameObject);
        }

        // if (hit by bullet)

        if (hp <= 0)
        {
            gameManager.ModifyPlayerValue(gameManager.playerMoney, GameManager.Modification.INCREASE, 
                gameManager.random.Next(minScrapOnDeath, maxScrapOnDeath + 1));
            Debug.Log("An enemy has been killed. Player Money will increase.");
            Debug.Log("Player Money is now: " + gameManager.playerMoney.value);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;  // most basic form, no modifiers into account ever
        Debug.Log("An enemy has been damaged. Enemy HP now: " + hp);
    }

    private void OnDestroy()
    {
        // any subscribers in the chat
    }
}
