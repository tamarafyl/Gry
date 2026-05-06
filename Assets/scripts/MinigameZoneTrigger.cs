using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MinigameZoneTrigger : MonoBehaviour
{
    [Header("Ustawienia sceny")]
    [Tooltip("Nazwa sceny z instrukcją dla tej konkretnej minigry")]
    public string nazwaScenyIntro; 

    [Header("Identyfikator minigry")]
    [Tooltip("Unikalna nazwa, np. 'Polowanie', 'Owoce', 'Hazard'")]
    public string idMinigry; 

    // Lista gier, które zostały już aktywowane, aby nie otwierały się ponownie
    private static HashSet<string> aktywowaneGry = new HashSet<string>();

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy obiekt, który wszedł w strefę, ma tag 'Player'
        if (other.CompareTag("Player"))
        {
            // Sprawdzamy, czy ta konkretna gra była już uruchomiona
            if (!aktywowaneGry.Contains(idMinigry))
            {
                aktywowaneGry.Add(idMinigry); // Zapamiętujemy aktywację
                Debug.Log("Uruchamianie minigry: " + idMinigry);
                
                // Ładowanie sceny z instrukcją
                SceneManager.LoadScene(nazwaScenyIntro);
            }
            else
            {
                Debug.Log("Ta minigra (" + idMinigry + ") została już ukończona lub otwarta wcześniej.");
            }
        }
    }
}