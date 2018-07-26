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
    public void SetCurrentShapeIndexToFirst()
    {
        orsom.shapeIndex = 0;
        print("0?"+orsom.shapeIndex);
    }
    public void SetCurrentShapeIndexToSecond()
    {
        orsom.shapeIndex = 1;
        print("1?" + orsom.shapeIndex);
    }
    public void SetCurrentShapeIndexToThird()
    {
        orsom.shapeIndex = 2;
        print("2?" + orsom.shapeIndex);
    }
}
