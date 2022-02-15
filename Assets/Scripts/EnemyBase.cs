using UnityEngine;
using Vuforia;

// Enemy that can be spawned from a start card,                                             -- EnemyManager/Spawner (not contained here)
// can walk forward with its typical speed from one waypoint passed to it to another,       -- EnemyMoveController (interface here, implement elsewhere)
// can die and update the player money on death,                                            -- this (HP check) + fire event on killed
// can reach goal, dying in the process and updating the player health on death.            -- this (interaction check) + fire event on goal/destroy
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float hp;
    [SerializeField] private float initSpeed;
    private float speed;
    [SerializeField] private int moneyOnDeath;

    private ITargetMoveController moveController;

    // ... would require that others keep an explicit reference to the enemy!
    public delegate void EnemyKilledEvent();
    public event EnemyKilledEvent OnEnemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        moveController.SetDestination(Vector3.zero /*first waypoint.position*/);
    }

    // Update is called once per frame
    void Update()
    {
        //WalkTowardsWaypoint();

        if (hp <= 0)
        {
            OnEnemyKilled?.Invoke();
            //enemy.OnEnemyKilled += (tower.)UpdateMoney(increase);
        }
    }
}
