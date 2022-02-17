using UnityEngine;
using Vuforia;

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

public class EnemyPathfindController : MonoBehaviour, IPathfindingController
{
    // Hardcoded limit of 10 per convention in Waypoint.cs and team agreement.
    // Physical cards CAN be moved and this should already update the enemy path, because it is based on the contained Waypoints' transform positions.

    private Waypoint[] waypointManager = new Waypoint[10];
    private Waypoint activeWaypoint = null;     // the waypoint the enemy immediately walks to next

    //public delegate void PathfinderReadyEvent(int id);
    //public event PathfinderReadyEvent OnPathfinderReady;

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

    public Waypoint GetActiveWaypoint()
    {
        return activeWaypoint;
    }

    /// <summary>
    /// In this implementation, sets active Waypoint to the next valid one within the list of this controller.
    /// Setting to an arbitrary one is not possible.
    /// </summary>
    /// <returns>
    /// True if setting was successful, false if no more valid Waypoints are found (in which case assume the enemy has reached the end).
    /// </returns>
    public bool SetActiveWaypoint()
    {
        Waypoint ogWP = activeWaypoint;

        int i = 1;
        do
        {
            activeWaypoint = waypointManager[activeWaypoint.orderID + i];
            i++;

            // break check after hitting the end of the array and revert to the original waypoint
            if (activeWaypoint.orderID + i >= waypointManager.Length)
            {
                activeWaypoint = ogWP;
                return false;
            }
        } while (activeWaypoint == null);

        return true;
    }
}
