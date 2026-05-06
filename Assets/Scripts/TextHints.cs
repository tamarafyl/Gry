using TMPro;
using UnityEngine;

public class TextHints : MonoBehaviour
{
    private TMP_Text guiText;
    private float timer = 0f;

    void Start()
    {
        guiText = GetComponent<TMP_Text>();
        guiText.enabled = false;
    }

    public void ShowHint(string message)
    {
        guiText.text = message;
        guiText.enabled = true;
        timer = 0f;
    }

    void Update()
    {
        if (guiText.enabled)
        {
            timer += Time.deltaTime;
            if (timer >= 4f)
            {
                guiText.enabled = false;
                timer = 0f;
            }
        }
    }
}
