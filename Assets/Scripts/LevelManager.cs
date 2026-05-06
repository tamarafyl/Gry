using UnityEngine;
using UnityEngine.SceneManagement; // Потрібно для зміни сцен

public class LevelManager : MonoBehaviour
{
    [Header("Налаштування сцени")]
    public string mainSceneName = "ForestScene"; // Точна назва вашої головної сцени лісу
    public float delayBeforeReturn = 3f;        // Пауза після вбивства останньої тварини

    private bool isLevelFinished = false;

    void Update()
    {
        // Якщо ми вже почали процес повернення, більше нічого не робимо
        if (isLevelFinished) return;

        // Шукаємо всі об'єкти з тегом "Animal"
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");

        // Якщо масив порожній (Length == 0), значить живих тварин не залишилося
        if (animals.Length == 0)
        {
            isLevelFinished = true;
            Debug.Log("Всі тварини переможені! Повернення через " + delayBeforeReturn + " сек.");
            
            // Викликаємо метод повернення з невеликою затримкою
            Invoke("ReturnToMainScene", delayBeforeReturn);
        }
    }

    void ReturnToMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}