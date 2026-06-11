using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{

    public string mainMenuSceneName = "Start"; 

    void Start()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}