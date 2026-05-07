using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject keyIcon; 
    void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        if (GameManager.instance != null && keyIcon != null)
        {
            
            keyIcon.SetActive(GameManager.instance.hasHuntingKey);
        }
    }
}