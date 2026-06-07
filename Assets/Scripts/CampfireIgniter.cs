using UnityEngine;

public class CampfireIgniter : MonoBehaviour
{
    [Header("Referencja do ognia POCHODNI")]
    [Tooltip("Przeciągnij tutaj obiekt ognia, który jest wyłączony na pochodni gracza")]
    public GameObject torchFire;

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy tylko, czy to gracz (zapalanie działa zawsze, gdy podejdziesz)
        if (other.CompareTag("Player"))
        {
            if (torchFire != null && !torchFire.activeSelf)
            {
                torchFire.SetActive(true); // Włączamy wizualny ogień na pochodni
                
                // Bezpiecznie informujemy GameManager (jeśli istnieje), że pochodnia płonie
                if (GameManager.instance != null)
                {
                    GameManager.instance.UpdateTorchStatus(true);
                }
                
                Debug.Log("Pochodnia została pomyślnie zapalona od ogniska!");
            }
        }
    }
}