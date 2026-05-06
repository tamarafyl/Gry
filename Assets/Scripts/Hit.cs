using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Hit : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameOverText.gameObject.SetActive(true);
            Invoke("BackToMenu", 1f);
        }
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
