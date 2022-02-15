using UnityEngine;
using Vuforia;

public interface IPathfindingController
{
    // enemy (move controller) must know its next waypoint
    // enemy (move controller) must move towards a destination (waypoint) (handled in the move interface)

    // enemy must be able to retrieve waypoint[next+1] from waypoint list, and update its waypoint[next] reference to it
    // EXTRA: update the waypoint[next] POSITION reference on some type of demand (or timer), in case card is moved out of place

    // Unity NavMeshPath contains a Vector3[].
    public void GetNextWaypoint();
    public void UpdateTargetWaypoint();
}

public class EnemyPathfindController : MonoBehaviour/*, IPathfindingController*/
{
    // Serialized for setting points manually (hardcoding).
    // Must implement detection of the physical card objects and their positions for dynamic (via cards) waypointing.

    // waypoint screening phase at start / between waves?

    [SerializeField] Vector3[] waypointManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
