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
    public static List<Vector3> leftSidePoints = new List<Vector3>();
    //-------------------
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
        //--------------------------------------------------------------

        drawLine(selectedPoints);

        //------------------------
        print("9adech famma fil left ?: "+leftSidePoints.Count);
        if (leftSidePoints.Count == 0)
        {
            drawFromTo(leftSidePoints[0], leftSidePoints[1]);
            drawFromTo(leftSidePoints[0], leftSidePoints[5]);

            drawFromTo(leftSidePoints[1], leftSidePoints[6]);
            drawFromTo(leftSidePoints[6], leftSidePoints[7]);
            drawFromTo(leftSidePoints[7], leftSidePoints[2]);
            drawFromTo(leftSidePoints[2], leftSidePoints[3]);
            drawFromTo(leftSidePoints[3], leftSidePoints[4]);
            drawFromTo(leftSidePoints[4], leftSidePoints[9]);
        }
        //drawFromTo(new Vector3(-10,-4,0),new Vector3(-10,-3,0));
        //------------------------
        
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
    //==================================================================
    public void drawLine(ArrayList twoPointsArrayList)
    {
        if (twoPointsArrayList.Count == 2)
        {
            Point posA = (Point)twoPointsArrayList[0];
            Point posB = (Point)twoPointsArrayList[1];

            float distance = Vector3.Distance(posA.transform.position, posB.transform.position);
            string dd = (distance == Mathf.Sqrt(2)) ? "SQRT(2)" : distance.ToString();
            print(dd);
            if (distance <= Mathf.Sqrt(2))
            {
                GameObject lrPf = Instantiate(lineRendererPF);
                lineRenderer = lrPf.GetComponent<LineRenderer>();


                lineRenderer.SetPosition(0, posA.transform.position);
                lineRenderer.SetPosition(1, posB.transform.position);

                twoPointsArrayList.RemoveAt(0);
            }
            else
            {
                twoPointsArrayList.RemoveAt(1);
                distance = 0;
            }

        }
    }
    public void drawFromTo(Vector3 from, Vector3 to) 
    {
        GameObject lrPf = Instantiate(lineRendererPF);
        lineRenderer = lrPf.GetComponent<LineRenderer>();


        lineRenderer.SetPosition(0, from);
        lineRenderer.SetPosition(1, to);
    }
}
