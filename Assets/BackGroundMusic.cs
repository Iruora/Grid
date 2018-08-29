using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour {
    bool isMute;
    private static BackGroundMusic instance = null;

    public static BackGroundMusic Instance
    {
        get { return instance;  }
    }
    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("music");
        if (gameObjects.Length > 1)
        {
            Destroy(this.gameObject);

        }
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
        print("started on awake music");
    }
    // Use this for initialization
    void Start () {
        print("started music");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }
}
