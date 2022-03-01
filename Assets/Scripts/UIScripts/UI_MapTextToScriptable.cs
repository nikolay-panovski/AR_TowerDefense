using UnityEngine;

public class UI_MapTextToScriptable : MonoBehaviour
{
    [SerializeField] private IntValue value;
    private TMPro.TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    // FixedUpdate for performance (vs who cares the number will not update instantly)
    private void FixedUpdate()
    {
        text.text = value.value.ToString();
    }
}
