using UnityEngine;

public class Clickable : MonoBehaviour
{
    private Camera mainCamera;
    public bool isClicked { get; private set; } = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //isClicked = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("Screen clicked at " + Input.mousePosition);

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject g = hit.collider.gameObject;
            if (g == this.gameObject)
            {
                isClicked = true;
            }
        }

        else
        {
            isClicked = false;
        }

        Debug.Log("isClicked: " + isClicked);
    }
}
