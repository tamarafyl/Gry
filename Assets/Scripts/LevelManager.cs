using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Налаштування сцени")]
    public string mainSceneName = "ForestScene"; 
    public float delayBeforeReturn = 3f;        

    private bool isLevelFinished = false;

    void Update()
    {
        if (isLevelFinished) return;

        // 1. Шукаємо всі об'єкти з тегом "Animal"
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");

        // 2. Якщо тварин не залишилося — гравець переміг
        if (animals.Length == 0)
        {
            isLevelFinished = true;

            // --- ДОДАНИЙ БЛОК ДЛЯ ТАЙМЕРА ---
            // Шукаємо таймер на сцені та вимикаємо його
            TimerManager timer = FindObjectOfType<TimerManager>();
            if (timer != null)
            {
                timer.enabled = false; 
                Debug.Log("Таймер зупинено! Перемога зафіксована.");
            }
            // --------------------------------
            if (GameManager.instance != null)
    {
        GameManager.instance.hasHuntingKey = true;
        Debug.Log("Ключ отримано та збережено в GameManager!");
    }

            Debug.Log("Всі тварини переможені! Повернення через " + delayBeforeReturn + " сек.");
            Invoke("ReturnToMainScene", delayBeforeReturn);
        }
    }

    void ReturnToMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}