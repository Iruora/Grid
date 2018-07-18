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
        pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        if (orsomInstance)
        orsom.Instance.Orsomli5at(transform.position, pz);
	}
    private void OnMouseDown()
    {
        print("Clickeni !");
        orsomInstance = true;
        orsom.Instance.mouseDown = true;
    }
    
    private void OnMouseUp()
    {
        print("Sayabni !");
        orsomInstance = false;
        orsom.Instance.mouseDown = false;
        orsom.Instance.Orsomli5at(transform.position, pz);

    }
}
