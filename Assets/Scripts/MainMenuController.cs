using UnityEngine;

public class MainMenuController : MonoBehaviour
{
  
    public GameObject instructionPanel;

    
    public void OpenInstruction()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true); 
        }
    }

   
    public void CloseInstruction()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false); 
        }
    }
}