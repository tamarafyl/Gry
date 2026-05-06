using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// wymagany komponent AudioSource
[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour
{
    // zmienne publiczne
    public AudioClip beep;
    public GUISkin menuSkin;
    public Rect menuArea;
    public Texture gameLogo;
    public Rect instructionsRect; // Dodana zmienna publiczna

    // prze³¹czniki skalowania
    public bool adjustPosition;
    public bool adjustSize;

    // prostok¹ty przycisków
    Rect playBtnRect;
    Rect instructionsBtnRect;
    Rect quitBtnRect;

    // parametry przycisków
    float buttonWidth = 200;
    float buttonHeight = 40;
    float space = 20;

    // wspó³czynniki skalowania
    float coefX = 1.0f;
    float coefY = 1.0f;

    // zmienna przechowuj¹ca bie¿¹c¹ stronê menu
    string menuPage = "main";

    void Start()
    {
        // skalowanie ROZMIARU menu
        if (adjustSize)
        {
            coefX = Screen.width / 1024.0f;
            coefY = Screen.height / 768.0f;
            menuArea.width *= coefX;
            menuArea.height *= coefY;
        }

        // skalowanie POZYCJI menu
        if (adjustPosition)
        {
            float w_2 = menuArea.width * 0.5f;
            float h_2 = menuArea.height * 0.5f;
            menuArea.x = (menuArea.x + w_2) * Screen.width / 1024 - w_2;
            menuArea.y = (menuArea.y + h_2) * Screen.height / 768 - h_2;
        }

        // przyciski
        playBtnRect = new Rect(
            50 * coefX,
            250 * coefY,
            buttonWidth * coefX,
            buttonHeight * coefY
        );

        instructionsBtnRect = new Rect(
            50 * coefX,
            (250 + buttonHeight + space) * coefY,
            buttonWidth * coefX,
            buttonHeight * coefY
        );

        quitBtnRect = new Rect(
            50 * coefX,
            (250 + (buttonHeight + space) * 2) * coefY,
            buttonWidth * coefX,
            buttonHeight * coefY
        );

        // Przesuwamy o 10 pikseli w prawo i dajemy du¿¹ wysokoœæ (300), ¿eby tekst mia³ gdzie spadaæ
        instructionsRect = new Rect(10 * coefX, 220 * coefY, 400 * coefX, 300 * coefY);
    }

    void OnGUI()
    {
        GUI.skin = menuSkin;
        GUI.BeginGroup(menuArea);

        // logo gry
        GUI.DrawTexture(
            new Rect(0, 0, 300 * coefX, 211 * coefY),
            gameLogo
        );

        // Sprawdzanie, któr¹ stronê menu wyœwietliæ
        if (menuPage == "main")
        {
            if (GUI.Button(playBtnRect, "Play"))
            {
                Debug.Log("Naciœniêto Play");
                StartCoroutine("ButtonAction", "SampleScene");
            }

            if (GUI.Button(instructionsBtnRect, "Instructions"))
            {
                Debug.Log("Naciœniêto Instructions");
                menuPage = "instructions"; // Przejœcie do strony instrukcji
                GetComponent<AudioSource>().PlayOneShot(beep);
            }

            if (GUI.Button(quitBtnRect, "Quit"))
            {
                Debug.Log("Naciœniêto Quit");
                StartCoroutine("ButtonAction", "quit");
            }
        }
        else if (menuPage == "instructions")
        {
            // Wyœwietlanie etykiety instrukcji
            GUI.Label(instructionsRect,
    "Obudzi³eœ siê na tajemniczej wyspie...\n" +
    "ZnajdŸ sposób na zwrócenie na siebie uwagi,\n" +
    "inaczej zostaniesz tu na zawsze!");

            // Przycisk powrotu (Back) w miejscu przycisku Quit
            if (GUI.Button(quitBtnRect, "Back"))
            {
                GetComponent<AudioSource>().PlayOneShot(beep);
                menuPage = "main"; // Powrót do menu g³ównego
            }
        }

        GUI.EndGroup();
    }

    // wspó³program obs³uguj¹cy akcje przycisków
    IEnumerator ButtonAction(string levelName)
    {
        GetComponent<AudioSource>().PlayOneShot(beep);
        yield return new WaitForSeconds(0.35f);

        if (levelName != "quit")
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}