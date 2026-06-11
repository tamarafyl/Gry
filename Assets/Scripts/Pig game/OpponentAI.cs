using UnityEngine;
using System.Collections;

public class OpponentAI : MonoBehaviour
{
    private PigGameManager _gameManager;

    [Header("Thrower Assignments")]
    [Tooltip("Drag the 'opponentThrower' GameObject here from the Hierarchy")]
    public DiceRoller OpponentThrower;

    [Header("AI Logic Settings")]
    [Tooltip("The AI will evaluate its risk options once it reaches this score")]
    public int TargetTurnScore = 18;

    [Tooltip("Chance (0.0 to 1.0) to keep rolling even after reaching the target score")]
    [Range(0f, 1f)]
    public float RiskAppetite = 0.5f;

    [Tooltip("Delay in seconds between AI decisions")]
    public float DecisionDelay = 1.5f;

    private void Awake()
    {
        _gameManager = GetComponent<PigGameManager>();
    }

    public void StartTurn()
    {
        StartCoroutine(ExecuteAILogic());
    }

    private IEnumerator ExecuteAILogic()
    {
        Debug.Log("[AI] Starting opponent's decision making process...");
        
        while (!_gameManager.IsPlayerTurn)
        {
            yield return new WaitForSeconds(DecisionDelay);

            int currentAIScore = _gameManager.CurrentTurnScore;

            // 1. Check if the AI has reached its standard comfortable score threshold
            if (currentAIScore >= TargetTurnScore)
            {
                // 2. Roll a virtual pseudo-random float between 0.0 and 1.0 to decide if the AI takes a risk
                float riskRoll = Random.value; 

                if (riskRoll <= RiskAppetite)
                {
                    // The AI feels lucky! It decides to ignore the safety threshold and rolls again
                    Debug.Log($"[AI] Threshold met ({currentAIScore}), but AI feels GREEDY! (Roll: {riskRoll:F2} <= {RiskAppetite}). Rolling again!");
                    ExecuteDiceThrow();
                }
                else
                {
                    // The AI decides to play it safe and secure the current points
                    Debug.Log($"[AI] Threshold met ({currentAIScore}) and AI chooses to play safe (Roll: {riskRoll:F2} > {RiskAppetite}). Choosing to HOLD.");
                    _gameManager.HoldPoints();
                    yield break; 
                }
            }
            else
            {
                // Standard behavior: if beneath the threshold, always roll
                Debug.Log($"[AI] Current turn score is {currentAIScore}. Under threshold. Rolling.");
                ExecuteDiceThrow();
            }

            // Wait for the physical simulation loop to fully complete before iterating the while loop
            yield return new WaitUntil(() => _gameManager.IsDiceRolling == false);
        }
    }

    // Helper method to encapsulate the throw registration logic safely
    private void ExecuteDiceThrow()
    {
        if (OpponentThrower != null)
        {
            _gameManager.NotifyDiceThrown();
            OpponentThrower.ThrowDice();
        }
        else
        {
            Debug.LogError("[AI] OpponentThrower reference is missing in the Inspector!");
        }
    }
}