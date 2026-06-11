using UnityEngine;
using UnityEngine.SceneManagement; // REQUIRED: For switching scenes

public class VictoryPortal : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("The exact name of your Victory/Win scene (e.g., 'Scene_Victory')")]
    public string VictorySceneName = "Scene_Victory";

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object entering the portal is the human player
        if (other.CompareTag("Player"))
        {
            Debug.Log("[PORTAL] Player stepped into the portal. Validating key inventory...");

            // 2. Access the persistent GameManager singleton instance
            if (GameManager.instance != null)
            {
                // 3. Read the status of all three required key booleans
                bool hasHunting = GameManager.instance.hasHuntingKey;
                bool hasGambling = GameManager.instance.hasGamblingKey;
                bool hasFalling = GameManager.instance.hasFallingKey;

                // 4. Verify if ALL 3 keys have been successfully collected
                if (hasHunting && hasGambling && hasFalling)
                {
                    Debug.Log("[PORTAL] All 3 keys are present! Initiating victory scene transition...");
                    
                    // Load the victory destination layout scene
                    SceneManager.LoadScene(VictorySceneName);
                }
                else
                {
                    // Calculate missing keys for a precise debug feedback log
                    Debug.Log($"[PORTAL] Access Denied! You don't have enough keys. Status -> Hunting: {hasHunting}, Gambling: {hasGambling}, Falling: {hasFalling}");
                    
                    // TODO: Trigger a UI text message on the screen here, e.g., "Find all 3 keys to open!"
                }
            }
            else
            {
                Debug.LogError("[PORTAL] Critical Error: No active GameManager instance found in the scene context!");
            }
        }
    }
}