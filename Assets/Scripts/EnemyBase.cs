using UnityEngine;
using Vuforia;

// Enemy that can be spawned from a start card,                                             -- EnemyManager/Spawner (not contained here)
// can walk forward with its typical speed from one waypoint passed to it to another,       -- EnemyMoveController (interface here, implement elsewhere)
// can die and update the player money on death,                                            -- this (HP check) + fire event on killed
// can reach goal, dying in the process and updating the player health on death.            -- this (interaction check) + fire event on goal/destroy
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Tooltip("Once an enemy is this close to its active waypoint, it will update and start moving to the next one.")]
    [SerializeField] private float minDistToWaypoint;

    [Header("Enemy stats")]
    [SerializeField] private float maxHP;
    private float hp;
    [SerializeField] private float initialSpeed;
    private float speed;
    [SerializeField] private int damageToGoal;
    [SerializeField] private int minScrapOnDeath;
    [SerializeField] private int maxScrapOnDeath;

    private ITargetMoveController moveController;
    private IPathfindingController pathfindController;

    private bool hasReachedGoal = false;

    // ... would require that others keep an explicit reference to the enemy!
    public delegate void EnemyKilledEvent();
    public event EnemyKilledEvent OnEnemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        // replace NULL checks with assertion of setting controllers to enemy or auto-setter
        if (moveController != null && pathfindController != null)
        {
            moveController.SetDestination(pathfindController.GetActiveWaypoint().GetPosition());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distVec;
        moveController.MoveTowardsDestination(speed, out distVec);
        if (distVec.magnitude < minDistToWaypoint)
        {
            hasReachedGoal = !pathfindController.SetActiveWaypoint();
            // **if the enemy path does not change on card move or seems broken, then we'd need to set the destination constantly in Update()
            moveController.SetDestination(pathfindController.GetActiveWaypoint().GetPosition());
        }

        if (hasReachedGoal)
        {
            //enemy.OnEnemyReachedGoal()    ## += (gamemanager.)UpdatePlayerHP(decrease);
            gameManager.ModifyPlayerValue(gameManager.playerHP, GameManager.Modification.DECREASE, damageToGoal);
        }

        if (hp <= 0)
        {
            OnEnemyKilled?.Invoke();
            //enemy.OnEnemyKilled()     ## += (gamemanager.)UpdateMoney(increase);
            gameManager.ModifyPlayerValue(gameManager.playerMoney, GameManager.Modification.INCREASE, 
                gameManager.random.Next(minScrapOnDeath, maxScrapOnDeath));
        }
    }

    private void OnDestroy()
    {
        
    }
}
