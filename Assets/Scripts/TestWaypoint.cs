using UnityEngine;

public class TestWaypoint : MonoBehaviour
{
    [field: SerializeField] public int orderID { get; private set; } = -1;

    // Start is called before the first frame update
    private void Start()
    {

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
