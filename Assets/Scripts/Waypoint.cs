using System;
using UnityEngine;
using Vuforia;

// Container for the position of a dedicated ImageTarget object (physical cards) for enemy pathfinding.
// Attach as a child GameObject to an ImageTarget.

public class Waypoint : MonoBehaviour
{
    private ImageTargetBehaviour parentTarget;
    private string imageTargetName;
    public int orderID { get; private set; } = -1;

    private void Start()
    {
        bool gotImageTarget;

        gotImageTarget = transform.parent.TryGetComponent<ImageTargetBehaviour>(out parentTarget);

        if (!gotImageTarget)
        {
            Debug.LogError("No ImageTarget got, Waypoint will not be able to get ID!");
        }

        parentTarget.OnTargetStatusChanged += OnGotTracked;
    }

    private void OnGotTracked(ObserverBehaviour ob, TargetStatus status)
    {
        if (orderID == -1)
        //if ((status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED) && status.StatusInfo == StatusInfo.NORMAL)
        {
            Debug.Log("Waypoint tracked!");

            getIDFromParentImage();
        }
    }

    // crude manual way to obtain reference to the image target and get order number from it, hardcoding in multiple aspects
    // (image target names when uploading to target manager, immutable order, limited number of objects of one type etc.)
    private void getIDFromParentImage()
    {
        imageTargetName = parentTarget.TargetName;
        // limits implementation to "imagefilename_IDhere.img" where IDhere = 0~9 only, for a max of 10 waypoints
        bool parseSuccess;
        int tempID;

        Debug.Log("Obtained imageTargetName: " + imageTargetName);
        string stringedID = imageTargetName.Substring(imageTargetName.Length - 1);
        Debug.Log("Obtained ID in string form (before TryParse): " + stringedID);

        parseSuccess = Int32.TryParse(stringedID, out tempID);
        if (parseSuccess)
        {
            orderID = tempID;
            Debug.Log("Final int orderID: " + orderID);
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
