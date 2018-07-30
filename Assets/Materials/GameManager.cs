using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject EndGamePanel;
    public GameObject MainMenuPanel;
    public Text MovesText;
    
    public void StartGame()
    {
        MainMenuPanel.SetActive(false);
        MovesText.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoHome()
    {
        print("LoadMode");
        orsom.instance.enabled = true;
        
        SceneManager.LoadScene(0);

        
    }

}
