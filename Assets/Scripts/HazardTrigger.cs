using UnityEngine;

public class HazardTrigger : MonoBehaviour
{
    private TreeHazard _parentTrap;

    private void Start()
    {
        // Automatically find the trap script on the parent object
        _parentTrap = GetComponentInParent<TreeHazard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the entity entering the trigger area is the human player
        if (other.CompareTag("Player"))
        {
            if (_parentTrap != null)
            {
                _parentTrap.ActivateTrap();
                
                // Turn off this trigger immediately so it doesn't fire multiple times
                gameObject.SetActive(false);
            }
        }
    }
}