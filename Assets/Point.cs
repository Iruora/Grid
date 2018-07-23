using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static List<VectorMath> rightSideLinks = new List<VectorMath>();

    public Text movesText;



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

            //----------------------------------------
            VectorMath AB = new VectorMath(posA.transform.position,posB.transform.position);
            //----------------------------------------

            float distance = Vector3.Distance(posA.transform.position, posB.transform.position);
            string dd = (distance == Mathf.Sqrt(2)) ? "SQRT(2)" : distance.ToString();
            //print(dd);
            if (distance <= Mathf.Sqrt(2) && !(rightSideLinks.Contains(AB)) && !(rightSideLinks.Contains(AB.reverse())))
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
                VectorMath vector = new VectorMath(posA.transform.position, posB.transform.position);
                //print(Vector3.Cross(new Vector3(2,2,2), new Vector3(-1, 1, 1)));
                //vectors.Add(posB.transform.position);
                rightSideLinks.Add(vector);


                //print(rightSideLinks.Count + " < ? " + currentshape.getPointsLinks().Count);
                if( rightSideLinks.Count <= currentshape.getPointsLinks().Count)
                {
                    print(rightSideLinks.Count+" <= ? "+currentshape.getPointsLinks().Count);
                    //--------------------------------
                    movesText.text = "Moves : "+(currentshape.getPointsLinks().Count - rightSideLinks.Count ); 
                    //--------------------------------
                    string stringResult = winGame()?"You win :)":"You lose,Try again :(";
                    if(rightSideLinks.Count == currentshape.getPointsLinks().Count)
                    {
                        print(stringResult);
                    }
                }
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
        List<VectorMath> pointsLinks = shape.getPointsLinks();
        for (int i = 0; i < shape.getPointsLinks().Count; i++)
        {
           

            Vector3 posA = (pointsLinks[i]).StartPoint;
            Vector3 posB = (pointsLinks[i]).EndPoint;
            
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
    bool winGame()
    {
        List<VectorMath> leftSideLinks = currentshape.getPointsLinks();
        List<bool> bools = new List<bool>();
        bool result = true;
        //print("==========================");
        leftSideLinks.ForEach(
            e => {
                bool b = false;
                b = rightSideLinks.Contains(e.getSymetric()) || rightSideLinks.Contains(e.reverse().getSymetric());
                bools.Add(b);
                //print(b);
            }    
        );
        //print("==========================");
        for (int i = 0; i < bools.Count; i++)
        {
            result &= bools[i];
        }
        return result;
    }
}
//======================================================
/************************ShapeClass************************/
//======================================================
public class Shape
{
    private LineRenderer lineRenderer;
    public GameObject lineRendererPF;//lineRenderPrefab

    private List<Vector3> pointsList = new List<Vector3>();
    private List<VectorMath> pointsLinks = new List<VectorMath> ();

    public void setPointsList(List<Vector3> pointsList) {
        this.pointsList.AddRange(pointsList);
    }
    //==============================================
    public List<Vector3> getPointsList() { return pointsList; }
    //==============================================
    public void setPointsLinks(List<VectorMath> pointsLinks)
    {
        this.pointsLinks.AddRange(pointsLinks);
    }
    //==============================================
    public List<VectorMath> getPointsLinks() { return pointsLinks; }
    
    //===============================================
    public void prepareLinks(int[] links)
    {
        for (int i = 0; i < links.Length- 1; i++)
        {
            VectorMath vect = new VectorMath(getPointsList()[links[i]], getPointsList()[links[i + 1]]);
            //vect.Add(getPointsList()[links[i]]);
            //vect.Add(getPointsList()[links[i+1]]);
            getPointsLinks().Add(vect);
        }
    }
}
//================================================
/************************ShapeClass************************/
//================================================
public class VectorMath
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    //----------------------------
    public VectorMath(Vector3 startPoint, Vector3 endPoint)
    {
        this.StartPoint = startPoint;
        this.EndPoint = endPoint;
    }
    //==============================================
    public Vector3 StartPoint
    {
        get
        {
            return startPoint;
        }

        set
        {
            startPoint = value;
        }
    }
    //==============================================
    public Vector3 EndPoint
    {
        get
        {
            return endPoint;
        }

        set
        {
            endPoint = value;
        }
    }
    //===============================================
    public VectorMath reverse()
    {
        return new VectorMath(this.endPoint,this.startPoint);
    }
    //===============================================
    public bool isSymetric(VectorMath vector) {
        return (
                startPoint.x == -(vector.startPoint.x) &&
                endPoint.x == -(vector.endPoint.x)
            );
    }
    //==============================================
    public override bool Equals(object obj)
    {
        var math = obj as VectorMath;
        return math != null &&
               EqualityComparer<Vector3>.Default.Equals(startPoint, math.startPoint) &&
               EqualityComparer<Vector3>.Default.Equals(endPoint, math.endPoint) &&
               EqualityComparer<Vector3>.Default.Equals(StartPoint, math.StartPoint) &&
               EqualityComparer<Vector3>.Default.Equals(EndPoint, math.EndPoint);
    }
    //===============================================
    public VectorMath getSymetric()
    {
        Vector3 start = this.startPoint;
        Vector3 end = this.endPoint;

        start.x = -(start.x);
        end.x = -(end.x);
        return new VectorMath(start,end);
    }
    //===============================================
    public override string ToString()
    {
        return "#"+startPoint+" ----> "+endPoint+"#";
    }

    public override int GetHashCode()
    {
        var hashCode = 433774470;
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(startPoint);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(endPoint);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(StartPoint);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(EndPoint);
        return hashCode;
    }
    //===============================================

}