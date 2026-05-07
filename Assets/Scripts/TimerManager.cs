using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class TimerManager : MonoBehaviour
{
    [Header("Налаштування часу")]
    public float timeRemaining = 60f;
    public Slider timeBar;

    [Header("Налаштування переходу")]
    public string gameOverSceneName = "GameOverScene"; // Назва вашої сцени смерті
    public float delayBeforeGameOver = 1f;           // Невелика затримка перед завантаженням

    private bool isGameOver = false;

    void Start()
    {
        if (timeBar != null)
        {
            timeBar.maxValue = timeRemaining;
            timeBar.value = timeRemaining;
        }
    }

    void Update()
    {
        // Якщо час вже вийшов і ми вантажимо сцену — виходимо
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeBar != null)
            {
                timeBar.value = timeRemaining;
            }
        }
        else
        {
            // Час вийшов
            timeRemaining = 0;
            isGameOver = true;
            
            Debug.Log("Час закінчився! Перехід до сцени смерті через " + delayBeforeGameOver + " сек.");
            
            // Викликаємо метод завантаження сцени з затримкою (як у вашому коді)
            Invoke("LoadGameOverScene", delayBeforeGameOver);
        }
    }

    void LoadGameOverScene()
    {
        // Вмикаємо курсор, щоб у сцені смерті можна було натискати кнопки
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(gameOverSceneName);
    }
}