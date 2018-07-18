using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    public bool orsomInstance;
    private Vector3 pz;
    //------------------
    private LineRenderer lineRenderer;
    public static int totalClicks = 0;
    public static ArrayList selectedPoints = new ArrayList();
    public GameObject lineRendererPF;//lineRenderPrefab

    //------------------


    // Use this for initialization
    void Start()
    {

    }
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the mouse position
        pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        //--------------------------------------------------------------
        
        if (selectedPoints.Count == 2  )
        {
            Point posA = (Point)selectedPoints[0];
            Point posB = (Point)selectedPoints[1];

            float distance = Vector3.Distance(posA.transform.position, posB.transform.position);
            string dd = (distance == Mathf.Sqrt(2)) ? "SQRT(2)" : distance.ToString();
            print(dd);
            if(distance <= Mathf.Sqrt(2))
            {
                GameObject lrPf = Instantiate(lineRendererPF);
                lineRenderer = lrPf.GetComponent<LineRenderer>();


                lineRenderer.SetPosition(0, posA.transform.position);
                lineRenderer.SetPosition(1, posB.transform.position);

                selectedPoints.Clear();
            }
            else
            {
                selectedPoints.RemoveAt(1);
                distance = 0;
            }
            
        }
        
    }
    //=================================================================
    private void OnMouseDown()
    {
        totalClicks++;
        selectedPoints.Add(this);
        print("selected :" + selectedPoints.Count);
    }
    //==================================================================
    private void OnMouseUp()
    {
        

    }
    //==================================================================
    public Vector3 getPosition()
    {
        return this.transform.position;
    }
}
