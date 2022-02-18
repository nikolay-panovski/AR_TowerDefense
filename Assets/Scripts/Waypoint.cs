using System;
using UnityEngine;
using Vuforia;

// Container for the position of a dedicated ImageTarget object (physical cards) for enemy pathfinding.
// Attach as a child GameObject to an ImageTarget.
public class Waypoint : MonoBehaviour
{
    private /*IPathfindingController*/ EnemyPathfindController pathfindingController;

    private ImageTargetBehaviour parentTarget;
    private string imageTargetName;
    public int orderID { get; private set; } = -1;

    [SerializeField] private bool enable = false;

    // for dirty testing only
    private bool testAlreadyEnabledOnce = false;
    private float countToFourSeconds = 0.0f;

    private void Start()
    {
        bool gotPathfController;

        gotPathfController = TryGetComponent<EnemyPathfindController>(out pathfindingController);

        parentTarget = transform.parent.GetComponent<ImageTargetBehaviour>();
        //parentTarget.OnTargetStatusChanged += TestPrintPosition;

        if (pathfindingController == null)
        {
            Debug.LogWarning("One or more Waypoints do not have a Pathfinding Controller assigned to their scripts!");
        }
    }

    private void Update()
    {
        if (enable) enabled = true;

        // force enable scripts after 4 seconds for phone test purposes
        if (!testAlreadyEnabledOnce && Time.time - countToFourSeconds > 4.0f)
        {
            enable = true;
            testAlreadyEnabledOnce = true;
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
        bool parseSuccess;
        int tempID;

        parseSuccess = Int32.TryParse(imageTargetName.Substring(imageTargetName.Length - 2), out tempID);
        if (parseSuccess)
        {
            orderID = tempID;
        }
        else
        {
            Debug.LogError("Image filename parsing for Waypoint ID failed. Did you follow the format 'name_0~9' for the filename?");
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
