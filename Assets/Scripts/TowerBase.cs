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

[RequireComponent(typeof(Clickable))]        // TEMP -- NO AR
[RequireComponent(typeof(Tappable))]
[RequireComponent(typeof(TowerEnemyChecker))]
[RequireComponent(typeof(TowerAttackController))]
public class TowerBase : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject UIButtonObject;
    private UI_TowerTradeButton UIButtonScript;
    private Renderer[] towerPartsRenderers;
    private /*IEnemyChecker*/ TowerEnemyChecker enemyChecker;
    private /*IAttackController*/ TowerAttackController attackController;

    [SerializeField] private TextureContainer texture;

    private Clickable clickable;
    private Tappable tappable;

    public bool isBought = false;  // isActive
    private bool wasBoughtLastFrame;
    private bool canAttack = false;

    [Header("Tower stats (also look at other scripts!)")]
    [SerializeField] private float attackCooldown;
    private float currentCooldown;
    [field: SerializeField] public int towerCost { get; private set; }
    //damage - to TowerAttackController
    //range - to TowerEnemyChecker


    // Start is called before the first frame update
    void Start()
    {
        UIButtonScript = UIButtonObject.GetComponent<UI_TowerTradeButton>();
        towerPartsRenderers = GetComponentsInChildren<Renderer>();

        /**/
        bool gotClickable, gotTappable, gotChecker, gotAttacker;
        gotClickable = TryGetComponent<Clickable>(out clickable);

        if (gotClickable == false)
        {
            Debug.LogError("Clickable component not found, attach one!");
        }

        gotTappable = TryGetComponent<Tappable>(out tappable);

        if (gotTappable == false)
        {
            Debug.LogError("Tappable component not found, attach one!");
        }

        gotChecker = TryGetComponent<TowerEnemyChecker>(out enemyChecker);

        if (gotChecker == false)
        {
            Debug.LogError("TowerEnemyChecker component not found, attach one!");
        }

        gotAttacker = TryGetComponent<TowerAttackController>(out attackController);

        if (gotAttacker == false)
        {
            Debug.LogError("TowerAttackController component not found, attach one!");
        }
        /**/
        foreach (Renderer renderer in towerPartsRenderers)
        {
            renderer.material.SetTexture("_BaseMap", null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: check if a conditional to tap detection is necessary
        bool isTapped = tappable.DetectTaps();

        if (isBought == false)
        {
            //if (clickable.isClicked)
            if (isTapped)
            {
                UIButtonObject.SetActive(false);
                UIButtonScript.callingTowerBase = this;
                UIButtonObject.SetActive(true);
            }

            if (wasBoughtLastFrame != isBought)
            {
                foreach (Renderer renderer in towerPartsRenderers)
                {
                    renderer.material.SetTexture("_BaseMap", null);
                }
            }
        }

        else //if (isBought)
        {
            //if (clickable.isClicked)
            if (isTapped)
            {
                UIButtonObject.SetActive(false);
                UIButtonScript.callingTowerBase = this;
                UIButtonObject.SetActive(true);
            }

            if (wasBoughtLastFrame != isBought)
            {
                foreach (Renderer renderer in towerPartsRenderers)
                {
                    renderer.material.SetTexture("_BaseMap", texture.cachedTexture);
                }
            }

            // also check for suitable conditionals, related to being visible
            canAttack = incrementAndCheckCooldown();

            if (canAttack)
            {
                enemyChecker.CheckForEnemiesInRange();

                // AoE version
                foreach (EnemyBase enemy in enemyChecker.detectedEnemies)
                {
                    attackController.PerformAttack(enemy);
                }

                // for single projectile version, PerformAttack(detectedEnemies(0)) outside of foreach

                canAttack = false;  // just in case
            }
        }

        wasBoughtLastFrame = isBought;
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

    // debug for tower range
    private void OnDrawGizmosSelected()
    {
        if (enemyChecker != null)
        {
            Gizmos.color = new Color(1, 1, 1, 0.9f);
            Gizmos.DrawSphere(transform.position, enemyChecker.range);
        }
    }
}
