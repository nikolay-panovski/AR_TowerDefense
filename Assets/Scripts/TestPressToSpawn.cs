using UnityEngine;
using Vuforia;

public class TestPressToSpawn : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject tapTarget;
    private ImageTargetBehaviour parentImageTarget;

    // Start is called before the first frame update
    void Start()
    {
        parentImageTarget = GetComponent<ImageTargetBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == tapTarget)
                {
                    Debug.Log("Okay!");
                    // spawn virtual button(s)
                    parentImageTarget.CreateVirtualButton("debugOutput",
                        new Vector3(0.025f, 0f, 0.075f), new Vector2(0.04f, 0.04f));

                    Debug.Log("Okay2");
                    parentImageTarget.CreateVirtualButton("leftSideButton",
                        new Vector3(0f, 0.001f, 0f), new Vector2(0.04f, 0.04f));

                    Debug.Log("Okay3");
                    parentImageTarget.CreateVirtualButton("rightSideButton",
                        new Vector3(-0.025f, 0.002f, 0.075f), new Vector2(0.04f, 0.04f));

                    Debug.Log("Okayge");

                    foreach (VirtualButtonBehaviour vb in parentImageTarget.GetVirtualButtonBehaviours())
                    {
                        Debug.Log("Virtual Button: " + vb.VirtualButtonName + " with BR: " + vb.Area.BottomRight + " and TL: " + vb.Area.TopLeft);
                        vb.RegisterOnButtonPressed(DebugOnVirtualPress);
                    }

                }

                //Destroy(child);
            }
        }
    }

    public void DebugOnVirtualPress(VirtualButtonBehaviour vb)
    {
        Debug.Log("VB pressed but only string without params");
        //Debug.Log("VB " + vb.VirtualButtonName + " pressed!");
    }
}
