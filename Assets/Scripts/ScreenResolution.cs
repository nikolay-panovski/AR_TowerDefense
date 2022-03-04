using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [SerializeField] private int width = 1920;
    [SerializeField] private int height = 1080;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(width, height, true);
    }
}
