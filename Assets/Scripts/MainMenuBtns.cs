using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Wymaga, aby obiekt mia³ komponent AudioSource
[RequireComponent(typeof(AudioSource))]
public class MainMenuBtns : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Publiczne zmienne do przypisania w Inspectorze
    public string levelToLoad;         // Nazwa sceny do wczytania
    public Sprite normalTexture;       // Tekstura przycisku w stanie normalnym
    public Sprite rollOverTexture;     // Tekstura przycisku po najechaniu kursorem
    public AudioClip beep;             // DŸwiêk klikniêcia
    public bool quitButton = false;    // Czy przycisk ma zakoñczyæ grê

    // Obs³uga podœwietlenia przy najechaniu kursorem
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = rollOverTexture;
    }

    // Przywrócenie normalnej tekstury, gdy kursor opuszcza przycisk
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = normalTexture;
    }

    // Wywo³anie akcji po zwolnieniu przycisku myszy
    public void OnPointerUp(PointerEventData eventData)
    {
        if (quitButton)
        {
            // Jeœli przycisk Quit, zakoñcz grê
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else
        {
            // Odtwarzanie dŸwiêku klikniêcia
            GetComponent<AudioSource>().PlayOneShot(beep);

            // Wczytanie sceny
            SceneManager.LoadScene(levelToLoad);
        }
    }

    // Pusta obs³uga zdarzenia PointerDown (wymagana, aby dzia³a³o PointerUp)
    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
