using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // REQUIRED: For scene transitions and reloads

public class PigGameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI ScoreText;
    public Button RollButton;
    public Button HoldButton;

    [Header("Game Over UI Panels")]
    [Tooltip("Panel shown when the human player wins")]
    public GameObject VictoryPanel;
    
    [Tooltip("Panel shown when the Ghost AI wins")]
    public GameObject DefeatPanel;

    [Header("Game Rules")]
    public int TargetScoreToWin = 100;

    [Header("Game State")]
    private int _playerScore = 0;
    private int _opponentScore = 0;
    private int _currentTurnScore = 0;
    private bool _playerTurn = true;
    private bool _isDiceRolling = false;
    private bool _isGameOver = false;

    public bool IsPlayerTurn => _playerTurn;
    public int CurrentTurnScore => _currentTurnScore;
    public bool IsDiceRolling => _isDiceRolling;

    [Header("Thrower Assignments")]
    public DiceRoller PlayerThrower;

    private OpponentAI _opponentAI;

    private void Start()
    {
        _opponentAI = GetComponent<OpponentAI>();
        
        // Ensure both ending overlays are hidden at setup
        if (VictoryPanel != null) VictoryPanel.SetActive(false);
        if (DefeatPanel != null) DefeatPanel.SetActive(false);
        
        UpdateScoreUI();
        UpdateControlInputs();
    }

    public void ProcessDiceResult(int diceResult)
    {
        if (_isGameOver) return;

        _isDiceRolling = false;
        if (diceResult == 1)
        {
            _currentTurnScore = 0;
            _playerTurn = !_playerTurn;
            
            UpdateScoreUI();
            UpdateControlInputs();
            CheckAndTriggerAITurn();
        }
        else
        {
            _currentTurnScore += diceResult;
            UpdateScoreUI();
            UpdateControlInputs();
        }
    }

    public void HoldPoints()
    {
        if (_isGameOver) return;

        if (_playerTurn) _playerScore += _currentTurnScore;
        else _opponentScore += _currentTurnScore;
        
        _currentTurnScore = 0;

        if (CheckVictoryConditions()) return;

        _playerTurn = !_playerTurn; 
        
        UpdateScoreUI();
        UpdateControlInputs();
        CheckAndTriggerAITurn();
    }

    public void NotifyDiceThrown()
    {
        if (_isGameOver) return;
        _isDiceRolling = true;
        UpdateControlInputs();
    }

    public void RegisterPlayerRollAction()
    {
        if (!_playerTurn || _isDiceRolling || _isGameOver) return;
        NotifyDiceThrown();
        if (PlayerThrower != null) PlayerThrower.ThrowDice();
    }

    private bool CheckVictoryConditions()
    {
        if (_playerScore >= TargetScoreToWin)
        {
            TriggerGameOver(isPlayerWinner: true);
            return true;
        }
        if (_opponentScore >= TargetScoreToWin)
        {
            TriggerGameOver(isPlayerWinner: false);
            return true;
        }
        return false;
    }

    private void TriggerGameOver(bool isPlayerWinner)
    {
        _isGameOver = true;

        // Block main gameplay inputs
        if (RollButton != null) RollButton.interactable = false;
        if (HoldButton != null) HoldButton.interactable = false;

        // Activate the mathematically correct UI overlay based on the outcome
        if (isPlayerWinner)
        {
            Debug.Log("[GAME OVER] Human wins! Displaying Victory UI.");
            if (VictoryPanel != null) VictoryPanel.SetActive(true);
        }
        else
        {
            Debug.Log("[GAME OVER] Ghost wins! Displaying Defeat UI.");
            if (DefeatPanel != null) DefeatPanel.SetActive(true);
        }
    }

    // BUTTON ACTION: Reloads the current match scene
    public void RestartGame()
    {
        Debug.Log("[GAME] Restarting match...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // BUTTON ACTION: Loads the previous scene in the build hierarchy index
    public void ExitToPreviousScene()
    {
        int previousSceneIndex = 1;//SceneManager.GetActiveScene().buildIndex - 1;
        if (GameManager.instance != null)
        {
            GameManager.instance.hasGamblingKey = true;
            Debug.Log("Ключ отримано та збережено в GameManager!");
        }
        // Safety check to ensure we don't try to load a negative scene index
        if (previousSceneIndex >= 0)
        {
            Debug.Log($"[GAME] Exiting to previous scene index: {previousSceneIndex}");
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogWarning("[GAME] No previous scene exists in Build Settings! Falling back to application quit.");
            Application.Quit(); // Fallback if playing a built standalone game
        }
    }

    private void CheckAndTriggerAITurn()
    {
        if (_isGameOver) return;
        if (!_playerTurn && _opponentAI != null) _opponentAI.StartTurn();
    }

    private void UpdateControlInputs()
    {
        if (_isGameOver) return;
        bool allowPlayerInput = _playerTurn && !_isDiceRolling;
        if (RollButton != null) RollButton.interactable = allowPlayerInput;
        if (HoldButton != null) HoldButton.interactable = allowPlayerInput;
    }

    private void UpdateScoreUI()
    {
        if (ScoreText != null)
        {
            if (_playerTurn) {
                ScoreText.text = $"Score\nPlayer: {_playerScore}+{_currentTurnScore}\nGhost: {_opponentScore}";
            }
            else {
                ScoreText.text = $"Score\nPlayer: {_playerScore}\nGhost: {_opponentScore}+{_currentTurnScore}";
            }
        }
    }
}