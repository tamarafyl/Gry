using UnityEngine;

public class DiceResultChecker : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _hasLanded = false;
    private bool _isTracking = false;
    private float _trackingDelayTimer = 0f;

    // A small buffer time (in seconds) to let the dice leave the hand and start tumbling
    private const float LaunchDelay = 0.3f; 

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // If the dice hasn't been thrown yet, do nothing
        if (!_isTracking) return;

        // Count down the initial launch delay safely
        if (_trackingDelayTimer > 0f)
        {
            _trackingDelayTimer -= Time.deltaTime;
            return;
        }

        // Check if the physics body has completely stopped moving and spinning
        if (!_hasLanded && _rb.linearVelocity.sqrMagnitude < 0.005f && _rb.angularVelocity.sqrMagnitude < 0.005f)
        {
            _hasLanded = true;
            _isTracking = false; // Stop monitoring until the next throw
            
            int finalResult = GetTopSide();
            Debug.Log($"[DICE] Stable Landing Detected! Result: {finalResult}");
            
            // Find the PigGameManager on the scene and pass the result to it
            PigGameManager manager = Object.FindFirstObjectByType<PigGameManager>();
            if (manager != null)
            {
                manager.ProcessDiceResult(finalResult);
            }
        }
    }

    // This is called by DiceRoller immediately when the dice is thrown
    public void ResetDiceState()
    {
        _hasLanded = false;
        _isTracking = true;
        _trackingDelayTimer = LaunchDelay; // Wait 0.3 seconds before checking speed
    }

    private int GetTopSide()
    {
        // Vector3.up is the global world "Sky" direction (0, 1, 0)
        // We compare the local orientations of your custom 3D model pivots against the sky
        
        float dotUp = Vector3.Dot(transform.up, Vector3.up);
        float dotForward = Vector3.Dot(transform.forward, Vector3.up);
        float dotRight = Vector3.Dot(transform.right, Vector3.up);

        // Check Local Y Axis (Based on your info: 2 is Up, 5 is Down)
        if (dotUp > 0.6f) return 2;
        if (dotUp < -0.6f) return 5;

        // Check Local Z Axis (Based on your info: 1 is Forward, 6 is Back)
        if (dotForward > 0.6f) return 1;
        if (dotForward < -0.6f) return 6;

        // Check Local X Axis (Based on your info: 4 is Right, 3 is Left)
        if (dotRight > 0.6f) return 4;
        if (dotRight < -0.6f) return 3;

        // Absolute fallback safety return
        return 1;
    }
}