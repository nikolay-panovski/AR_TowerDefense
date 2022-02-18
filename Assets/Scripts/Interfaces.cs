using UnityEngine;

public interface ITargetMoveController
{
    public void SetDestination(Vector3 dest);
    public void MoveTowardsDestination(float speed, out Vector3 distVec);
}

public interface IPathfindingController
{
    // enemy (move controller) must know its next waypoint
    // enemy (move controller) must move towards a destination (waypoint) (handled in the move interface)

    // EXTRA: update the waypoint[next] POSITION reference on some type of demand (or timer), in case card is moved out of place

    // Unity NavMeshPath contains a Vector3[] corners (waypoints). However the movement is done internally -
    // it is not guaranteed they are using methods as below. Especially if a form of node graph is used there but not here.
    public Waypoint GetActiveWaypoint();    // dependency on a class Waypoint existing
    public bool SetActiveWaypoint();        // returns whether it was successful or not
    public void AddWaypointToList(Waypoint wp);
}

public interface IEnemyChecker
{
    public void CheckForEnemiesInRange();
}

public interface IAttackController
{

}