using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject keyIcon; // Перетягни сюди об'єкт KeyIcon в інспекторі

    void Start()
    {
        // При завантаженні сцени перевіряємо GameManager
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        if (GameManager.instance != null && keyIcon != null)
        {
            // Показуємо іконку, лише якщо ключ є в GameManager
            keyIcon.SetActive(GameManager.instance.hasHuntingKey);
        }
    }
}