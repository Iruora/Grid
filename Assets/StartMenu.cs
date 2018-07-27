using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public GameObject MainMenuPanel;
    // Use this for initialization
    void Start () {
        
        print("started");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetCurrentShapeIndexToFirst()
    {
        orsom.shapeIndex = 0;
        print("0?" + orsom.shapeIndex);
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
