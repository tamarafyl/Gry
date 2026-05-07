using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Прогрес гравця")]
    public bool hasHuntingKey = false; // Чи отримав гравець ключ за полювання

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
        // При новому старті гри ключа немає
        hasHuntingKey = false; 
        PlayerPrefs.SetInt("ShouldRestorePosition", 0);
        PlayerPrefs.Save();
    }
}