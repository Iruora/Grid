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
                }
                //-------------------
                else 
                {
                    //print("( "+i+ "," +j+" ) :=> "+obj.transform.position);
                    
                    Point.leftSidePoints.Add(obj.transform.position);
                }
                //-------------------
            }
        }
        
        print("Count : "+Point.leftSidePoints.Count);
        Point.leftSidePoints.ForEach(e => print("Pos X :"+e));
    }
}
