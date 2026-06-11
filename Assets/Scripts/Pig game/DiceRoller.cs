using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [Header("Dice References")]
    [Tooltip("The actual dice GameObject present in the scene (NOT a prefab)")]
    public Rigidbody TargetDice;
    
    [Tooltip("The position from where the dice is thrown")]
    public Transform SpawnPoint;

    [Header("Throw Forces")]
    public float ForwardForce = 1f;
    public float UpwardForce = 0f;
    public float MaxTorque = 5f;

    // Call this function to re-throw the same dice
    public void ThrowDice()
    {
        if (TargetDice == null)
        {
            Debug.LogError("TargetDice is not assigned in the Inspector!");
            return;
        }

        // 1. Move the existing dice back to the starting spawn position
        TargetDice.transform.position = SpawnPoint.position;
        
        // 2. Give it a random starting rotation so it lands differently every time
        TargetDice.transform.rotation = Random.rotation;

        // 3. CRITICAL: Reset physical velocities, otherwise old movement forces will accumulate
        TargetDice.linearVelocity = Vector3.zero;
        TargetDice.angularVelocity = Vector3.zero;

        // 4. Calculate the directional arc vector
        Vector3 throwDirection = (SpawnPoint.forward * ForwardForce) + (Vector3.up * UpwardForce);

        // 5. Apply forces to the same physics body again
        TargetDice.AddForce(throwDirection, ForceMode.Impulse);

        // 6. Apply random torque spin
        Vector3 randomTorque = new Vector3(
            Random.Range(-MaxTorque, MaxTorque),
            Random.Range(-MaxTorque, MaxTorque),
            Random.Range(-MaxTorque, MaxTorque)
        );
        TargetDice.AddTorque(randomTorque, ForceMode.Impulse);

        // 7. Reset the landing tracking logic on the dice itself
        if (TargetDice.TryGetComponent<DiceResultChecker>(out var checker))
        {
            checker.ResetDiceState();
        }
    }
}