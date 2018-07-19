using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Point : MonoBehaviour
{
    public bool orsomInstance;
    private Vector3 pz;
    //------------------
    private LineRenderer lineRenderer;
    private Shape currentshape;

    public static int totalClicks = 0;
    public static ArrayList selectedPoints = new ArrayList();

    public static List<Vector3> leftSidePoints = new List<Vector3>();
    public static List<Vector3> symAxePoints = new List<Vector3>();
    public static List<Shape> leftSideShapes = new List<Shape>();

    public static List<Vector3> rightSidePoints = new List<Vector3>();
    public static List<List<Vector3>> rightSideLinks = new List<List<Vector3>>();




    
    //-------------------
    public GameObject lineRendererPF;//lineRenderPrefab
    //private GameObject lrPf;

    //------------------


    // Use this for initialization
    void Start()
    {
        //Instantiate(lineRendererPF);
        drawSymAxe();

        if (leftSidePoints.Count != 0)
        {
            
            prepareLeftSideForms();
            //waht shape to draw ?
            currentshape = leftSideShapes[1];
            drawShape(currentshape);

        }
    }
    //====================================================
    private void Awake()
    {
    }
    //====================================================
    // Update is called once per frame
    void Update()
    {
         drawLine(selectedPoints);
        
    }
    //=================================================================
    private void OnMouseDown()
    {
        totalClicks++;
        selectedPoints.Add(this);
        //print("selected :" + selectedPoints.Count);
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
            //print(dd);
            if (distance <= Mathf.Sqrt(2))
            {
                GameObject lrPf = Instantiate(lineRendererPF);
                lineRenderer = lrPf.GetComponent<LineRenderer>();


                lineRenderer.SetPosition(0, posA.transform.position);
                lineRenderer.SetPosition(1, posB.transform.position);
                /**/
                //print(posA.transform.position);
                twoPointsArrayList.RemoveAt(0);
                //-------------------------------------------------------
                rightSidePoints.Add(posA.transform.position);
                rightSidePoints.Add(posB.transform.position);
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                List<Vector3> vectors = new List<Vector3>();
                //print(Vector3.Cross(new Vector3(2,2,2), new Vector3(-1, 1, 1)));
                vectors.Add(posB.transform.position);
                rightSideLinks.Add(vectors);

                rightSideLinks.ForEach(
                        e =>
                        {
                            //print(currentshape.getPointsLinks().Contains(e));
                        }
                    );
            }
            else
            {
                twoPointsArrayList.RemoveAt(1);
                distance = 0;
            }

        }
    }
    //======================================================
    public void drawFromTo(Vector3 from, Vector3 to) 
    {
        GameObject lrPf = Instantiate(lineRendererPF);
        lineRenderer = lrPf.GetComponent<LineRenderer>();

        float distance = Vector3.Distance(from, to);
        if (distance <= Mathf.Sqrt(2))
        {
            lineRenderer.SetPosition(0, from);
            lineRenderer.SetPosition(1, to);
        }
            
    }
    //=======================================================
    public void drawSymAxe()
    {
        for (int i = 0; i < symAxePoints.Count - 1; i++)
        {
            
            drawFromTo(symAxePoints[i], symAxePoints[i + 1]);
        }
    }
    //========================================================
    public void drawShape(Shape shape)
    {
        List<Vector3> pointsList = shape.getPointsList();
        List<List<Vector3>> pointsLinks = shape.getPointsLinks();
        for (int i = 0; i < shape.getPointsLinks().Count; i++)
        {
           

            Vector3 posA = (pointsLinks[i])[0];
            Vector3 posB = (pointsLinks[i])[1];
            
            drawFromTo(posA,posB);


        }
    }
    //========================================================

    public void prepareLeftSideForms()
    {
        //initiating shape point's links 
        int[] firstShapeLinks = { 10, 5, 1, 2, 7, 6, 11, 12, 8, 3, 9, 14 };
        int[] secondShapeLinks = { 10, 5, 0, 1, 6, 7, 2, 3, 4, 9, 14 };
        int[] thirdShapeLinks = { 11, 5, 0, 1, 7, 2, 3, 4, 9, 13 };
        List<Vector3> vectors = new List<Vector3>();
        List<List<Vector3>> vect = new List<List<Vector3>>();
        
        //shapePoints gathers all of the points forming the shape
        List<Vector3> shapePoints = new List<Vector3>();
        //First Shape
        Shape firstShape = new Shape();
        
        //Gathering shape's points including those of the symAxe
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);
        //Setting shape points lit
        firstShape.setPointsList(shapePoints);
        firstShape.prepareLinks(firstShapeLinks);

        
        ///******************************************************************
        //******************************************************************
        //Second Shape
        Shape secondShape = new Shape();
        //
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);

        secondShape.setPointsList(shapePoints);
        secondShape.prepareLinks(secondShapeLinks);
        //******************************************************************
        //******************************************************************
        //third Shape
        Shape thirdShape = new Shape();
        //
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);

        thirdShape.setPointsList(shapePoints);
        thirdShape.prepareLinks(thirdShapeLinks);
        //--------------filling the list of the premade left shapes----------------------
        leftSideShapes.Add(firstShape);
        leftSideShapes.Add(secondShape);
        leftSideShapes.Add(thirdShape);
        //------------------------------------
    }
    //========================================================
}
public class Shape
{
    private LineRenderer lineRenderer;
    public GameObject lineRendererPF;//lineRenderPrefab

    private List<Vector3> pointsList = new List<Vector3>();
    private List<List<Vector3>> pointsLinks = new List<List<Vector3>> ();

    public void setPointsList(List<Vector3> pointsList) {
        this.pointsList.AddRange(pointsList);
    }
    //==============================================
    public List<Vector3> getPointsList() { return pointsList; }
    //==============================================
    public void setPointsLinks(List<List<Vector3>> pointsLinks)
    {
        this.pointsLinks.AddRange(pointsLinks);
    }
    //==============================================
    public List<List<Vector3>> getPointsLinks() { return pointsLinks; }
    
    //===============================================
    public void prepareLinks(int[] links)
    {
        for (int i = 0; i < links.Length- 1; i++)
        {
            List<Vector3> vect = new List<Vector3>();

            vect.Add(getPointsList()[links[i]]);
            vect.Add(getPointsList()[links[i+1]]);

            getPointsLinks().Add(vect);

        }
    }
}
