using UnityEngine;
using Vuforia;

public class TestWaypointOutput : MonoBehaviour
{
    private ImageTargetBehaviour imageTargetBehaviour;

    private static int waypointCount = 0;
    //[SerializeField] private float cooldown = 2.0f;

    private void Start()
    {
        imageTargetBehaviour = GetComponent<ImageTargetBehaviour>();
        imageTargetBehaviour.OnTargetStatusChanged += TestPrintPosition;
    }

    public void TestPrintPosition(ObserverBehaviour ob, TargetStatus status)
    {
        if (status.Status == Status.TRACKED && status.StatusInfo == StatusInfo.NORMAL)
        {
            waypointCount++;
            Debug.Log("Waypoint " + waypointCount + " position: " + transform.position);
        }
    }
}
