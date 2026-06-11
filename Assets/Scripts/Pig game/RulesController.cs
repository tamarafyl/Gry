using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    [Header("UI Target")]
    [Tooltip("Drag the RulesPanel (the object with text) here")]
    public GameObject Rules;
    public GameObject Score;

    // This function will automatically invert the current visibility state of the panel
    public void TogglePanel()
    {
        if (Rules == null || Score == null)
        {
            Debug.LogError("[UI] TargetPanel variable is not assigned in the Inspector!");
            return;
        }

        // Check the current active state and set the opposite value
        Rules.SetActive(!Rules.activeSelf);
        Score.SetActive(!Score.activeSelf);
        
    }
}