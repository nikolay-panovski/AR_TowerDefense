using UnityEngine;

public class UI_VerifyGameButton : MonoBehaviour
{
    // it is only this late that I realize the gameManager should have been a true singleton
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Canvas ownCanvas;
    [SerializeField] private Canvas mainCanvas;

    private TMPro.TMP_Text buttonText;

    private void Start()
    {
        buttonText = GetComponentInChildren<TMPro.TMP_Text>();
    }

    public void VerifyWaypoints()
    {
        // this code is very dirty.
        foreach (Waypoint wp in FindObjectsOfType<Waypoint>())
        {
            if (wp.orderID != -1)
            {
                ownCanvas.gameObject.SetActive(false);
                mainCanvas.gameObject.SetActive(true);
                gameManager.isGameValid.value = true;
                //buttonText.text = "Play";

                break;
            }

            else
            {
                buttonText.text = "Try again";
            }
        } 
    }
}
