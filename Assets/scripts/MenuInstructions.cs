using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInstrukcji : MonoBehaviour
{
   
    public void RozpocznijGre(string nazwaSceny)
    {
        SceneManager.LoadScene(nazwaSceny);
    }
}