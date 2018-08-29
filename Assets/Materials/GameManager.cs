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
        BackGroundMusic instance = BackGroundMusic.Instance;
        if (instance != null)
        {
            instance.Mute();
        }
        SceneManager.LoadScene(0);

        
    }

    public void MuteInMain()
    {
        BackGroundMusic instance = BackGroundMusic.Instance;
        if ( instance != null)
        {
            instance.Mute();
        }
    }

}
