using System;
using UnityEngine;
using Vuforia;

public class TestShowcaseEnemy : MonoBehaviour
{
    // needs X ImageTargets in database -> consider available printed cards

    [SerializeField] private float globalSpeed;
    [Header("Distance between Enemy and Waypoint at which the enemy will stop trying to get closer and will go for the next Waypoint. Adjust slightly.")]
    [SerializeField] private float arrivalMargin = 0.064f;

    // PATHFINDING section
    [SerializeField] private ImageTargetBehaviour[] imageTargetGoals;
    private MeshRenderer[] imageTargetRenderers;
    // for once, bad decoupling
    [SerializeField] private GameObject[] augmentationObjects;
    [SerializeField] private string[] targetedImageNames;
    private int currentWP;

    [SerializeField] private float maxHP;
    private float hp;

    void Start()
    {
        currentWP = 0;

        hp = maxHP;

        for (int i = 0; i < imageTargetGoals.Length; i++)
        {
            imageTargetRenderers[i] = imageTargetGoals[i].GetComponent<MeshRenderer>();
        }
    }

    void Update()
    {
        // detection of the other object as target and move towards there
        if (imageTargetRenderers[currentWP].enabled == true)
        {
            moveTowardsWaypoint();
        }   
    }

    private void moveTowardsWaypoint()
    {
        Vector3 WPPosition = augmentationObjects[currentWP].transform.position;
        Vector3 thisPosition = this.transform.position;

        Vector3 distVec = WPPosition - thisPosition;
        distVec.Normalize();
        distVec *= globalSpeed * 0.01f;

        this.transform.position += distVec * Time.deltaTime;

        if (Vector3.Distance(WPPosition, thisPosition) < arrivalMargin)
        {
            Debug.Log("Passed through waypoint!");
            updateCurrentWaypoint();
        }
    }

    private void updateCurrentWaypoint()
    {
        if (currentWP < imageTargetGoals.Length - 1)
        {
            currentWP++;
            Debug.Log("Waypoint updated.");
        }
        else
        {
            Debug.Log("An enemy reached last waypoint. Enemy wins, Game Over."); // BELOW MINIMAL game over!
            Destroy(gameObject);
        }
    }

    public void DecreaseHP(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Debug.Log("An enemy has died. Game Won!"); // BELOW MINIMAL game win!
            Destroy(gameObject);
        }
    }
}
