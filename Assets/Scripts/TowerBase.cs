using System;
using UnityEngine;

// Tower that shows up from a specific tower card, as greyed out/inactive                   -- 
// can be pressed on to make a menu (or button group) show up                               -- Tappable component + a spawner of buttons
// can be bought (on inactive) and sold (on active)                                         -- comms with GameManager
// can have its extra ability activated (on active + cooldown)                              -- 
// (EXTRA: ability costs money too + activation by voice instead of a button?)
// can check whether there are any enemies in its range                                     -- 
// can shoot out an AttackType (projectile or AoE) on a cooldown                            -- AttackSpawner
// AttackType must be changeable/flexible, as some ideas include chain of proj->AoE, or ability temp switching from proj to AoE
// Attack ->

[RequireComponent(typeof(Tappable))]
public class TowerBase : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private /*IEnemyChecker*/ TowerEnemyChecker enemyChecker;
    private /*IAttackController*/ TowerAttackController attackController;

    private Tappable tappable;

    private bool isBought = false;  // isActive
    private bool canAttack = false;

    [Header("Tower stats")]
    [SerializeField] private int towerCost;
    [SerializeField] private float baseDamage;
    private float damage;                           // extra damage variable because of variable damage in abilities
    [SerializeField] private float attackCooldown;
    private float currentCooldown;
    [SerializeField] private float range;           // enum (scriptable object) for short/medium/long?



    // Start is called before the first frame update
    void Start()
    {
        bool gotTappable;
        gotTappable = TryGetComponent<Tappable>(out tappable);

        if (gotTappable == false)
        {
            Debug.LogError("Tappable component not found, attach one!");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: check if a conditional to tap detection is necessary
        // bool moment = 
        tappable.DetectTaps();


        if (isBought)
        {
            // also check for suitable conditionals, related to being visible
            canAttack = incrementAndCheckCooldown();
        }
        
        if (canAttack)
        {
            //attackController.CreateAttack();
        }
    }

    private bool incrementAndCheckCooldown()
    {
        currentCooldown += Time.deltaTime;

        if (currentCooldown >= attackCooldown)
        {
            currentCooldown = 0.0f;
            return true;
        }

        return false;
    }
}
