using UnityEngine;

[RequireComponent(typeof(EnemyPathfindController))]
public class TestWaypoint : MonoBehaviour
{
    private EnemyPathfindController pathfindingController;

    [field: SerializeField] public int orderID { get; private set; } = -1;

    // Start is called before the first frame update
    private void Start()
    {
        bool gotPathfController;

        gotPathfController = TryGetComponent<EnemyPathfindController>(out pathfindingController);

        if (pathfindingController == null)
        {
            Debug.LogWarning("One or more Waypoints do not have a Pathfinding Controller assigned to their scripts!");
        }

        else
        {
            pathfindingController.TestAddWaypointToList(this);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
