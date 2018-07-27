using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orsom : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;

    public Vector3 startPosition;
    //------------------------------------
    private Point pointInstance;
    //------------------------------------
    public bool mouseDown;//defines wether the mouse is down or not
    public bool isDrawing;//defines wether we still drawing or not
    public bool lineRendererInited;//

    public GameObject lineRendererPF;//lineRenderPrefab
    public GameObject pointPf;//pointPrefab

    public int[] grid;
    
    //---------------------------------------------
    public GameObject endOfGamePanel;
    public Text resultMessage;
    //---------------------------------------------
    //---------------------26/07/2018--------------
    public static List<VectorMath> rightSideLinks = new List<VectorMath>();
    private Shape currentshape;

    public static List<Vector3> symAxePoints = new List<Vector3>();
    public static List<Shape> leftSideShapes = new List<Shape>();

    private LineRenderer lineRenderer;

    public Text moves;

    public static ArrayList selectedPoints = new ArrayList();

    public static List<Vector3> leftSidePoints = new List<Vector3>();


    public static List<Vector3> rightSidePoints = new List<Vector3>();

    public static int totalClicks = 0;
    //---------------------------------------------
    public static int shapeIndex = 0;
    //---------------------------------------------


    public static orsom instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }




    
     
    //============================================================================================
    void OnLevelWasLoaded()
    {
        
        print("orsom started");
        //instantiating the grid
        grid = new int[gridHeight * gridWidth];
        //filling the grid
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                //Intantiate a GameObject from the prefab "pointPf" and locate it at "startPosition" (initialisation in the unity editor)
                //with a translation forEach point by the vector u(i,i,0)
                GameObject obj = Instantiate(pointPf, startPosition + new Vector3(i, j, 0), Quaternion.identity);
                //-----------------------------------------
                //Add Circle colider only to the right side including the symetrie axe
                if (i >=  gridWidth / 2)
                {
                    //Singleton pattern : the circle colider will have a radius of 0.2f
                    obj.AddComponent<CircleCollider2D>().radius = 0.2f;
                    if (i == gridWidth / 2)
                    {
                        symAxePoints.Add(obj.transform.position);

                    }
                }
                //-------------------
                
                //-------------------
                else 
                {
                    leftSidePoints.Add(obj.transform.position);
                }
                //-------------------
            }
        }

        //=================25/07/2018=============
        endOfGamePanel.SetActive(false);
        drawSymAxe();
        if (leftSidePoints.Count != 0)
        {

            prepareLeftSideForms();
            //waht shape to draw ?
            //print("Index : "+shapeIndex);
            currentshape = leftSideShapes[shapeIndex];
            drawShape(currentshape);

        }
        moves.text = "Moves : " + (currentshape.getPointsLinks().Count);
        //========================================


    }
    //===================================================
    // Update is called once per frame
    void Update()
    {
        drawLine(selectedPoints);

    }
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }

    public void drawLine(ArrayList twoPointsArrayList)
    {
        if (twoPointsArrayList.Count == 2)
        {
            Point posA = (Point)twoPointsArrayList[0];
            Point posB = (Point)twoPointsArrayList[1];

            //----------------------------------------
            VectorMath AB = new VectorMath(posA.transform.position, posB.transform.position);
            //----------------------------------------

            float distance = Vector3.Distance(posA.transform.position, posB.transform.position);
            //string dd = (distance == Mathf.Sqrt(2)) ? "SQRT(2)" : distance.ToString();
            //print(dd);
            if (distance <= Mathf.Sqrt(2) && !(rightSideLinks.Contains(AB)) && !(rightSideLinks.Contains(AB.reverse())))
            {
                GameObject lrPf = Instantiate(lineRendererPF);
                lineRenderer = lrPf.GetComponent<LineRenderer>();


                lineRenderer.SetPosition(0, posA.transform.position);
                lineRenderer.SetPosition(1, posB.transform.position);
                
                twoPointsArrayList.RemoveAt(0);
                //twoPointsArrayList.Clear();
                //-------------------------------------------------------
                rightSidePoints.Add(posA.transform.position);
                rightSidePoints.Add(posB.transform.position);
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
                VectorMath vector = new VectorMath(posA.transform.position, posB.transform.position);
                
                rightSideLinks.Add(vector);
                
                if (rightSideLinks.Count <= currentshape.getPointsLinks().Count)
                {
                    //print("*************Right**************");
                    //rightSideLinks.ForEach(e => print(e));
                    //print("*************/Right**************");
                    //print("*************left**************");
                    //currentshape.getPointsLinks().ForEach(e => print(e.getSymetric()));
                    //print("*************/left**************");
                    //print(rightSideLinks.Count + " <= ? " + currentshape.getPointsLinks().Count);
                    //--------------------------------
                    moves.text = "Moves : " + (currentshape.getPointsLinks().Count - rightSideLinks.Count);
                    //--------------------------------
                    string stringResult = winGame() ? "You win :)" : "You lose\nTry again :(";
                    if (rightSideLinks.Count == currentshape.getPointsLinks().Count)
                    {
                        //print(stringResult);


                        resultMessage.text = stringResult;
                        Time.timeScale = 0f;
                        endOfGamePanel.gameObject.SetActive(true);
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

            
            drawFromTo(posA, posB);
            


        }
    }
    //========================================================

    public void prepareLeftSideForms()
    {
        //initiating shape point's links 
        /*
        int[] firstShapeLinks = { 10, 5, 1, 2, 7, 6, 11, 12, 8, 3, 9, 14 };
        int[] secondShapeLinks = { 10, 5, 0, 1, 6, 7, 2, 3, 4, 9, 14 };
        */
        //---------------------------------------------------------
        //---------------------25/07/2018--------------------------
        List<VectorMath> firstShapeLinksBis = new List<VectorMath>();
        firstShapeLinksBis.Add(new VectorMath(new Vector2(0, -2), new Vector2(-1, -2)));//10-5
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-1, -2), new Vector2(-2, -2)));//5-0
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-2, -2), new Vector2(-2, -1)));//0-1
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-2, -1), new Vector3(-1, -1)));//1-6
        firstShapeLinksBis.Add(new VectorMath(new Vector3(-1, -1), new Vector3(-1, -0)));//6-7
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-1, 0), new Vector2(-2, 0)));//7-2
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-2, 0), new Vector2(-2, 1)));//2-3
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-2, 1), new Vector2(-2, 2)));//3-4
        firstShapeLinksBis.Add(new VectorMath(new Vector2(-2, 2), new Vector2(-1, 2)));//4-9
        firstShapeLinksBis.Add(new VectorMath(new Vector3(-1, 2), new Vector3(0, 2)));//9-14
        //-----------------
        List<VectorMath> secondShapeLinksBis = new List<VectorMath>();
        secondShapeLinksBis.Add(new VectorMath(new Vector2(0, -2), new Vector2(-1, -2)));//10-5
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-1, -2), new Vector3(-2, -1)));//5-1
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-2, -1), new Vector3(-2, 0)));//1-2
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-2, 0), new Vector3(-1, 0)));//2-7
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-1, 0), new Vector3(-1, -1)));//7-6
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-1, -1), new Vector3(0, -1)));//6-11
        secondShapeLinksBis.Add(new VectorMath(new Vector3(0, -1), new Vector3(0, 0)));//11-12
        secondShapeLinksBis.Add(new VectorMath(new Vector3(0, 0), new Vector3(-1, 1)));//12-8
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-1, 1), new Vector3(-2, 1)));//8-3
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-2, 1), new Vector3(-1, 2)));//3-9
        secondShapeLinksBis.Add(new VectorMath(new Vector3(-1, 2), new Vector3(0, 2)));//9-14
        //-----------------
        List<VectorMath> thirdShapeLinksBis = new List<VectorMath>();
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(0, -1), new Vector2(-1, -2)));//11-5*
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-1, -2), new Vector2(-2, -2)));//5-0*
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-2, -2), new Vector2(-2, -1)));//0-1
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-2, -1), new Vector2(-1, 0)));//1-7
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-1, 0), new Vector2(-2, 0)));//7-2
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-2, 0), new Vector2(-2, 1)));//2-3
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-2, 1), new Vector2(-2, 2)));//3-4
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-2, 2), new Vector2(-1, 2)));//4-9
        thirdShapeLinksBis.Add(new VectorMath(new Vector2(-1, 2), new Vector2(0, 1)));//9-13

        //---------------------------------------------------------

        //shapePoints gathers all of the points forming the shape
        List<Vector3> shapePoints = new List<Vector3>();
        //First Shape
        Shape firstShape = new Shape();

        //Gathering shape's points including those of the symAxe
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);
        //Setting shape points lit
        firstShape.setPointsList(shapePoints);
        firstShape.prepareLinks(firstShapeLinksBis);


        ///******************************************************************
        //******************************************************************
        //Second Shape
        Shape secondShape = new Shape();
        //
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);

        secondShape.setPointsList(shapePoints);
        secondShape.prepareLinks(secondShapeLinksBis);
        //******************************************************************
        //******************************************************************
        //third Shape
        Shape thirdShape = new Shape();
        //
        shapePoints.AddRange(leftSidePoints);
        shapePoints.AddRange(symAxePoints);

        thirdShape.setPointsList(shapePoints);
        thirdShape.prepareLinks(thirdShapeLinksBis);
        //--------------filling the list of the premade left shapes----------------------
        leftSideShapes.Add(firstShape);
        leftSideShapes.Add(secondShape);
        leftSideShapes.Add(thirdShape);
        //------------------------------------
    }
    //========================================================
    private bool winGame()
    {
        List<VectorMath> leftSideLinks = currentshape.getPointsLinks();
        List<bool> bools = new List<bool>();
        bool result = true;
        
        leftSideLinks.ForEach(
            e => {
                bool b = false;
                b = rightSideLinks.Contains(e.getSymetric()) || rightSideLinks.Contains(e.reverse().getSymetric());
               
                bools.Add(b);
                //print(b);
            }
        );
        
        for (int i = 0; i < bools.Count; i++)
        {
            result &= bools[i];
        }
        return result;
    }
   //===================================================
}
//======================================================
/************************ShapeClass************************/
//======================================================
public class Shape
{
    private LineRenderer lineRenderer;
    public GameObject lineRendererPF;//lineRenderPrefab

    private List<Vector3> pointsList = new List<Vector3>();
    private List<VectorMath> pointsLinks = new List<VectorMath>();

    public void setPointsList(List<Vector3> pointsList)
    {
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
    public void prepareLinksBis(int[] links)
    {
        for (int i = 0; i < links.Length - 1; i++)
        {
            VectorMath vect = new VectorMath(getPointsList()[links[i]], getPointsList()[links[i + 1]]);
            getPointsLinks().Add(vect);
        }
    }
    //===============================================
    public void prepareLinks(List<VectorMath> links)
    {
        foreach (VectorMath vect in links)
        {
            //VectorMath vect = new VectorMath(getPointsList()[links[i]], getPointsList()[links[i + 1]]);
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

    float symAxeX = orsom.symAxePoints[0].x;
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
        return new VectorMath(this.endPoint, this.startPoint);
    }
    //===============================================
    public bool isSymetric(VectorMath vector)
    {
        return (
                symAxeX - startPoint.x == vector.startPoint.x &&
                symAxeX - endPoint.x == vector.endPoint.x
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

        
        //Debug.Log("symAxeX = "+symAxeX);
        start.x = start.x + 2 * ( symAxeX  - start.x);
        end.x = end.x + 2 * ( symAxeX - end.x);
        return new VectorMath(start, end);
    }
    //===============================================
    public override string ToString()
    {
        return "#" + startPoint + " ----> " + endPoint + "#";
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