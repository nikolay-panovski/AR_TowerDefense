using UnityEngine;

public class Tappable : MonoBehaviour
{
    //[SerializeField] private Camera AR_Camera;
    private Camera arCamera;

    private void Awake()
    {
        // !!this requires that there is only ARCamera in the scene, or at least it is the FIRST object of this type and tag!!
        arCamera = Camera.main;
    }

    /// <summary>
    /// The generic Unity raycasting way to detect taps on screen. Add to any object which needs to track tapping.
    /// </summary>
    /// <returns>True if a tap was detected on the appropriate GameObject, false otherwise.</returns>
    public bool DetectTaps()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // if possible, optimize Camera.main - however, is that possible? singleton - but how?
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject g = hit.collider.gameObject;
                if (g == this.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
