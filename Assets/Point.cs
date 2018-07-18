using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
    public bool orsomInstance;
    private Vector3 pz;

    // Use this for initialization
    void Start () {

    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update () {
        //Getting the mouse position
        pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        //--------------------------------------------------------------
        //Don't draw unless we click the Point
        if (orsomInstance)
            orsom.Instance.DrawLine(transform.position, pz);
	}
    //=================================================================
    private void OnMouseDown()
    {
        print("Clickeni !");
        orsomInstance = true;//Now you can draw !
        orsom.Instance.mouseDown = true;//the mouse is down
    }
    //==================================================================
    private void OnMouseUp()
    {
        print("Sayabni !");
        orsomInstance = false;//Stop drawing !
        orsom.Instance.mouseDown = false;//mouse is up
        orsom.Instance.DrawLine(transform.position, pz);//Finnally draw th line from me as a Point toward the mouse position

    }
}
