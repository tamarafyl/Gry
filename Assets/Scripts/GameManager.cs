using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Прогрес гравця")]
    public bool hasHuntingKey = false; 
    
    [Header("Стан світу")]
    public bool isDay = true;
    public bool isTorchBurning = false; 
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
        isTorchBurning = false; 
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

    public void PlayerDeath()
    {
        Debug.Log("Гравець загинув!");
        SceneManager.LoadScene("Scene_GameOver");
    }
}