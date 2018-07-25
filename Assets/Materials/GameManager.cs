using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
