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
    public Text text;
    //---------------------------------------------
    public GameObject endOfGamePanel;
    public Text resultMessage;


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
    void Start()
    {
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

                //-----------------------------------------
                //Add Circle colider only to the right side including the symetrie axe
                if (i >=  gridWidth / 2)
                {
                    //Singleton pattern : the circle colider will have a radius of 0.2f
                    obj.AddComponent<CircleCollider2D>().radius = 0.2f;
                    if (i == gridWidth / 2)
                    {
                        Point.symAxePoints.Add(obj.transform.position);

                    }
                }
                //-------------------
                
                //-------------------
                else 
                {
                    //print("( "+i+ "," +j+" ) :=> "+obj.transform.position);
                    
                    Point.leftSidePoints.Add(obj.transform.position);
                }
                //-------------------
            }
        }
        
        //print("Count : "+Point.leftSidePoints.Count);
        //Point.leftSidePoints.ForEach(e => print("Pos X :"+e));
    }
}
