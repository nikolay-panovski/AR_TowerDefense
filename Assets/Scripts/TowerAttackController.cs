using UnityEngine;

public class TowerAttackController : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    private float damage;                           // extra damage variable because of variable damage in abilities

    // Start is called before the first frame update
    void Start()
    {
        damage = baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformAttack(EnemyBase enemy)
    {
        enemy.TakeDamage(damage);
    }
}
