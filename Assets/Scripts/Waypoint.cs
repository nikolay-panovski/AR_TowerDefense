using System;
using UnityEngine;
using Vuforia;

// Attach as a child GameObject to an ImageTarget to enable simple pathfinding via physical cards.
public class Waypoint : MonoBehaviour
{
    [SerializeField] private /*IPathfindingController*/ EnemyPathfindController pathfindingController;

    private ImageTargetBehaviour parentTarget;
    private string imageTargetName;
    public int orderID { get; private set; } = -1;

    private void Start()
    {
        parentTarget = transform.parent.GetComponent<ImageTargetBehaviour>();
        parentTarget.OnTargetStatusChanged += TestPrintPosition;

        if (pathfindingController == null)
        {
            Debug.LogWarning("One or more Waypoints do not have a Pathfinding Controller assigned to their scripts in the Inspector!");
        }
    }

    // WHOLE OBJECT, OR THIS SCRIPT, HAS TO START AS DISABLED! else must find another trigger, consider OnTargetStatusChanged directly.
    private void OnEnable()
    {
        Debug.Log("Waypoint enabled!");
        
        getIDFromParentImage();

        pathfindingController.AddWaypointToList(this);
    }

    // crude manual way to obtain reference to the image target and get order number from it, hardcoding in multiple aspects
    // (image target names when uploading to target manager, immutable order, limited number of objects of one type etc.)
    private void getIDFromParentImage()
    {
        imageTargetName = parentTarget.TargetName;
        // limits implementation to "imagefilename_IDhere.img" where IDhere = 0~9 only, for a max of 10 waypoints
        orderID = Int32.Parse(imageTargetName.Substring(imageTargetName.Length - 2));
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void TestPrintPosition(ObserverBehaviour ob, TargetStatus status)
    {
        if (status.Status == Status.TRACKED && status.StatusInfo == StatusInfo.NORMAL)
        {
            
        }
    }
}
