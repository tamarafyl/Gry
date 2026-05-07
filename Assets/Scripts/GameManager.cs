using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Прогрес гравця")]
    public bool hasHuntingKey = false; 
    
    [Header("Час доби")]
    public bool isDay = true; // Змінна для тварин

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeGame()
    {
        hasHuntingKey = false; 
        PlayerPrefs.SetInt("ShouldRestorePosition", 0);
        PlayerPrefs.Save();
    }

    public void UpdateDayStatus(bool dayStatus)
    {
        if (isDay != dayStatus)
        {
            isDay = dayStatus;
            Debug.Log(isDay ? "Настав день!" : "Настала ніч!");
        }
    }
}