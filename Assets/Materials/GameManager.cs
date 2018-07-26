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
        
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        print("exec");
    }

    public void Reload()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
