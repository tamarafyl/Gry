using UnityEngine;

public class HUDController : MonoBehaviour
{
    // Static instance allowing other scripts to easily trigger UI refreshes
    public static HUDController instance;

    public GameObject keyIcon;
    public GameObject keyIcon2;
    public GameObject keyIcon3;

    void Awake()
    {
        // Set up the singleton instance layout
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        // Added safety checks for all individual icon references
        if (GameManager.instance != null)
        {
            if (keyIcon != null) keyIcon.SetActive(GameManager.instance.hasHuntingKey);
            if (keyIcon2 != null) keyIcon2.SetActive(GameManager.instance.hasGamblingKey);
            if (keyIcon3 != null) keyIcon3.SetActive(GameManager.instance.hasFallingKey);
            
            Debug.Log("[HUD] UI icons updated based on current GameManager state.");
        }
    }
}