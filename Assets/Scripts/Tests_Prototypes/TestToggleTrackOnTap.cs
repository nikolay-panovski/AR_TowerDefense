using UnityEngine;
using Vuforia;

public class TestToggleTrackOnTap : MonoBehaviour
{
    private bool isTrackingActive = true;
    private ImageTargetBehaviour[] trackers;

    private void Start()
    {
        trackers = FindObjectsOfType<ImageTargetBehaviour>(true);
    }

    // Update is called once per frame
    void Update()
    {
        bool wasTrackingActive = isTrackingActive;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isTrackingActive ^= true;
            Debug.Log("Image tracking is now " + isTrackingActive);
        }

        if (wasTrackingActive != isTrackingActive)
        {
            if (isTrackingActive)
            {
                foreach (ImageTargetBehaviour target in trackers)
                {
                    target.enabled = false;
                }
            }
            else
            {
                foreach (ImageTargetBehaviour target in trackers)
                {
                    target.enabled = true;
                }
            }
        }

        
    }
}
