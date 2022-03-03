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
            Debug.LogWarning("No ImageTarget got, Waypoint will not be able to get ID!");
        }
    }

    private void Update()
    {
        
    }

    public void TestEnable()
    {
        enabled = true;
    }

    // WHOLE OBJECT, OR THIS SCRIPT, HAS TO START AS DISABLED! else must find another trigger, consider OnTargetStatusChanged directly.
    private void OnEnable()
    {
        Debug.Log("Waypoint enabled!");
        
        getIDFromParentImage();
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
