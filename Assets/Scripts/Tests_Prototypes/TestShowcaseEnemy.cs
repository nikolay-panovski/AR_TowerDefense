using System;
using UnityEngine;
using Vuforia;

public class TestShowcaseEnemy : MonoBehaviour
{
    // needs X ImageTargets in database -> consider available printed cards

    [SerializeField] private float globalSpeed;
    [Tooltip("Distance between Enemy and Waypoint at which the enemy will stop trying to get closer and will go for the next Waypoint. Adjust slightly.")]
    [SerializeField] private float arrivalMargin = 0.064f;

    // PATHFINDING section
    [SerializeField] private ImageTargetBehaviour[] imageTargetGoals;
    [SerializeField] private GameObject[] augmentationObjects;
    private int currentWP;

    [SerializeField] private float maxHP;
    public float hp { get; private set; }

    void Start()
    {

        currentWP = 0;

        hp = maxHP;
    }

    void Update()
    {
        Status nextWPStatus = imageTargetGoals[currentWP].TargetStatus.Status;
        StatusInfo nextWPStatusInfo = imageTargetGoals[currentWP].TargetStatus.StatusInfo;

        if ((nextWPStatus == Status.TRACKED || nextWPStatus == Status.EXTENDED_TRACKED)
            && nextWPStatusInfo == StatusInfo.NORMAL)
        {
            moveTowardsWaypoint();
        }

        else
        {
            Debug.Log("waypoint " + currentWP + " ain't tracked");
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
