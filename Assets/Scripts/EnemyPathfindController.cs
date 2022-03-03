using UnityEngine;

public class EnemyPathfindController : MonoBehaviour
{
    // Hardcoded limit of 10 per convention in Waypoint.cs and team agreement.
    // Physical cards CAN be moved and this should already update the enemy path, because it is based on the contained Waypoints' transform positions.

    private Waypoint[] waypointManager = new Waypoint[10];
    private TestWaypoint[] testWaypointManager = new TestWaypoint[10];
    private Waypoint activeWaypoint = null;     // the waypoint the enemy immediately walks to next
    private TestWaypoint testActiveWaypoint = null;

    //public delegate void PathfinderReadyEvent(int id);
    //public event PathfinderReadyEvent OnPathfinderReady;

    private void Start()
    {
        
    }

    public void AddWaypointToList(Waypoint wp)
    {
        Debug.Log("Waypoint manager length = " + waypointManager.Length);
        Debug.Log("Trying to add WP with orderID: " + wp.orderID);
        if (waypointManager[wp.orderID] == null)
        {
            // add waypoint only if it is not already located (is NULL)
            waypointManager[wp.orderID] = wp;
            Debug.Log("Waypoint with ID " + wp.orderID + " added to controller list");
        }
    }

    public void AddWaypointToList(TestWaypoint wp)
    {
        if (testWaypointManager[wp.orderID] == null)
        {
            // I hope it's readable that this is a TEST METHOD (for the no AR)
            testWaypointManager[wp.orderID] = wp;
            //Debug.Log("TestWaypoint with ID " + wp.orderID + " added to controller list");
        }
    }

    public Waypoint GetActiveWaypoint()
    {
        return activeWaypoint;
    }

    public TestWaypoint GetActiveTestWaypoint()
    {
        return testActiveWaypoint;
    }

    // for TEST only, but will probably need the same thing for the first WP of the real one
    // "unsafe" so no information except the log returned if it fails.
    public void SetFirstActiveWaypoint()
    {
        int i = 0;
        do
        {
            activeWaypoint = waypointManager[i];
            i++;

            // break check after hitting the end of the array and revert to the original waypoint
            if (i >= waypointManager.Length)
            {
                Debug.LogError("There are no active Waypoints at all! Add some to the scene and put their IDs between 0 and 9!");
                break;
            }
        } while (activeWaypoint == null);

        Debug.Log("First active waypoint has ID: " + activeWaypoint.orderID);
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
        Waypoint nextWP = activeWaypoint;

        int i = 1;
        do
        {
            /**/
            if (activeWaypoint.orderID + i >= waypointManager.Length)
            {
                return false;
            }
            /**/

            nextWP = waypointManager[activeWaypoint.orderID + i];
            i++;

        } while (nextWP == null);

        activeWaypoint = nextWP;
        return true;
    }

    public bool TestSetActiveWaypoint()
    {
        TestWaypoint nextWP = testActiveWaypoint;

        int i = 1;
        do
        {
            /**/
            // break check after hitting the end of the array and do not set waypoint
            if (testActiveWaypoint.orderID + i >= testWaypointManager.Length)
            {
                return false;
            }
            /**/

            nextWP = testWaypointManager[testActiveWaypoint.orderID + i];
            i++;

        } while (nextWP == null);

        testActiveWaypoint = nextWP;
        //Debug.Log("Next active waypoint has ID: " + testActiveWaypoint.orderID);
        return true;
    }
}
