using UnityEngine;
using Vuforia;

public interface IPathfindingController
{
    // enemy (move controller) must know its next waypoint
    // enemy (move controller) must move towards a destination (waypoint) (handled in the move interface)

    // EXTRA: update the waypoint[next] POSITION reference on some type of demand (or timer), in case card is moved out of place

    // Unity NavMeshPath contains a Vector3[] corners (waypoints). However the movement is done internally -
    // it is not guaranteed they are using methods as below. Especially if a form of node graph is used there but not here.
    public void SetActiveWaypoint();
}

public class EnemyPathfindController : MonoBehaviour, IPathfindingController
{
    // Hardcoded limit of 10 per convention in Waypoint.cs and team agreement.
    // Must implement detection of the physical card objects and their positions for dynamic (via cards) waypointing.

    Waypoint[] waypointManager = new Waypoint[10];
    public Waypoint activeWaypoint { get; private set; } = null;     // the waypoint the enemy immediately walks to next

    public delegate void PathfinderReadyEvent(int id);
    public event PathfinderReadyEvent OnPathfinderReady;

    private void Start()
    {
        
    }

    public void AddWaypointToList(Waypoint wp)
    {
        if (waypointManager[wp.orderID] == null)
        {
            // for now at least: add waypoint only if it is not already located (is NULL)
            waypointManager[wp.orderID] = wp;
            Debug.Log("Waypoint with ID " + wp.orderID + " added to controller list");
        }
    }

    public void SetActiveWaypoint()
    {
        // activeWaypoint = next earliest non-NULL waypoint from array
    }
}
