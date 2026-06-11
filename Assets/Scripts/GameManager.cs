using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Postęp gracza")]
    public bool hasHuntingKey = false; 
    public bool hasGamblingKey = false;
    public bool hasFallingKey = false;
    
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

    // Subscribe to scene loading events when this object becomes active
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from events when the object is destroyed to prevent memory leaks
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method triggers automatically EVERY TIME a new scene finishes loading
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GAME] Scene '{scene.name}' loaded. Checking state requirements...");
        
        // Execute state validation for the torch
        CheckAndApplyTorchState();
    }

    void InitializeGame()
    {
        hasHuntingKey = false;
        hasGamblingKey = false;
        hasFallingKey = false;
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

    public void UpdateTorchStatus(bool torchStatus)
    {
        if (isTorchBurning != torchStatus)
        {
            isTorchBurning = torchStatus;
            Debug.Log(isTorchBurning ? "Pochodnia jest teraz zapalona!" : "Pochodnia zgasła!");
        }
    }

    // Evaluates if the torch needs to be ignited inside the newly loaded scene
    private void CheckAndApplyTorchState()
    {
        if (isTorchBurning)
        {
            GameObject torchObject = GameObject.FindWithTag("TorchFire");
            if (torchObject != null)
            {
                torchObject.SetActive(true); // Turn on the torch GameObject
            }
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Gracz zginął!");
        SceneManager.LoadScene("Scene_GameOver");
    }
}