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

    // Lista gier, które zostały już aktywowane
    private static HashSet<string> aktywowaneGry = new HashSet<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!aktywowaneGry.Contains(idMinigry))
            {
                // --- ЗБЕРЕЖЕННЯ ПОЗИЦІЇ ---
                PlayerPrefs.SetFloat("ReturnPosX", other.transform.position.x);
                PlayerPrefs.SetFloat("ReturnPosY", other.transform.position.y);
                PlayerPrefs.SetFloat("ReturnPosZ", other.transform.position.z);
                PlayerPrefs.SetFloat("ReturnRotY", other.transform.eulerAngles.y);
                PlayerPrefs.SetInt("ShouldRestorePosition", 1);
                PlayerPrefs.Save(); 

                // --- ДОДАНО: РОЗБЛОКУВАННЯ МИШІ ---
                // Це звільняє курсор від контролера гравця
                Cursor.lockState = CursorLockMode.None; 
                // Це робить стрілку миші видимою на екрані
                Cursor.visible = true; 

                aktywowaneGry.Add(idMinigry); 
                Debug.Log("Uruchamianie minigry: " + idMinigry + " | Pozycja zapisana | Mysz odblokowana.");
                
                SceneManager.LoadScene(nazwaScenyIntro);
            }
            else
            {
                Debug.Log("Ta minigra (" + idMinigry + ") została już ukończona.");
            }
        }
    }
}