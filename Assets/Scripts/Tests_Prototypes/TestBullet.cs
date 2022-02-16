using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] private float globalSpeed = 2f;
    [SerializeField] private float arrivalMargin = 0.064f;
    public int dmg { get; set; }

    [System.NonSerialized] public GameObject followEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followEnemy != null)
        {
            Move();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector3 enemyPosition = followEnemy.transform.position;
        Vector3 thisPosition = this.transform.position;

        Vector3 distVec = enemyPosition - thisPosition;
        distVec.Normalize();
        distVec *= globalSpeed * 0.01f;

        this.transform.position += distVec * Time.deltaTime;

        if (Vector3.Distance(enemyPosition, thisPosition) < arrivalMargin)
        {
            Debug.Log("Bullet hit enemy!");
            TestShowcaseEnemy enemyScript = followEnemy.GetComponent<TestShowcaseEnemy>();
            enemyScript.DecreaseHP(dmg);
            Debug.Log("Enemy HP down to: " + enemyScript.hp);
            Destroy(gameObject);
        }
    }
}
