using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Postęp gracza")]
    public bool hasHuntingKey = false; 
    
    [Header("Stan świata")]
    public bool isDay = true;
    public bool isTorchBurning = false; 
    public int currentAttackersCount = 0;

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
            Debug.Log(isDay ? "Nastał dzień!" : "Nastała noc!");
        }
    }

    // Nowa metoda do aktualizacji stanu pochodni z innych skryptów
    public void UpdateTorchStatus(bool torchStatus)
    {
        if (isTorchBurning != torchStatus)
        {
            isTorchBurning = torchStatus;
            Debug.Log(isTorchBurning ? "Pochodnia jest teraz zapalona!" : "Pochodnia zgasła!");
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Gracz zginął!");
        SceneManager.LoadScene("Scene_GameOver");
    }
}