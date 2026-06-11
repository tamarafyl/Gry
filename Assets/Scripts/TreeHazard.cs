using UnityEngine;
using UnityEngine.SceneManagement; // REQUIRED: For switching to the death scene

public class TreeHazard : MonoBehaviour
{
    [Header("Death Scene Settings")]
    [Tooltip("The exact name of your Game Over / Death scene (e.g., 'DeathScene')")]
    public string DeathSceneName = "DeathScene";

    [Tooltip("Minimum falling speed required to kill the player")]
    public float LethalSpeedThreshold = 3f;

    private Rigidbody _rb;
    private bool _isFalling = false;
    private bool _hasKilled = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        // Double-check to ensure physics don't accidentally pull it down prematurely
        if (_rb != null)
        {
            _rb.useGravity = false;
        }
    }

    // This method will be triggered by the child TriggerZone script when the player walks under
    public void ActivateTrap()
    {
        if (_isFalling) return;

        _isFalling = true;
        if (_rb != null)
        {
            _rb.useGravity = true; // Unleash physics gravity to make the object fall
            Debug.Log($"[TRAP] {gameObject.name} activated! Object is now falling.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isFalling || _hasKilled) return;

        // Calculate the impact velocity magnitude specifically on the vertical axis
        float impactSpeed = Mathf.Abs(collision.relativeVelocity.y);

        if (impactSpeed >= LethalSpeedThreshold)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                _hasKilled = true;
                Debug.Log($"[TRAP] Player crushed by {gameObject.name} at {impactSpeed:F2} m/s! Loading death scene...");
                
                // Switch immediately to your dedicated death scene
                SceneManager.LoadScene(DeathSceneName);
            }
        }

        // Optional: Destroy the falling fruit/branch after it hits the ground to clean up the scene
        if (collision.gameObject.CompareTag("Untagged") || collision.gameObject.layer == 0) // Default ground
        {
            Destroy(gameObject, 1f); // Disappear after 1 second on the floor
        }
    }
}