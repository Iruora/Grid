using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orsom : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;

    public Vector3 startPosition;

    public bool mouseDown;//defines wether the mouse is down or not
    public bool isDrawing;//defines wether we still drawing or not
    public bool lineRendererInited;//

    public GameObject lineRendererPF;//lineRenderPrefab
    public GameObject pointPf;//pointPrefab

    public int[] grid;
    
    private static orsom _instance;//
    //================================***************=============================================
    public static orsom Instance {
        get {
            return _instance;
        }
    }
    //============================================================================================

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
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
                
                //Add Circle colider only to the right side including the symetrie axe
                if (i >= (int)gridWidth / 2)
                {
                    //Singleton pattern : the circle colider will have a radius of 0.2f
                    obj.AddComponent<CircleCollider2D>().radius = 0.2f;
                }
            }
        }
    }
    //==============================Draws a line between posA and posB=============================
    public void DrawLine(Vector3 posA, Vector3 posB)
    {

        if (mouseDown)
        {

            if (!isDrawing)
            {
                GameObject lrPF = Instantiate(lineRendererPF);

                LineRenderer lineRenderer = lrPF.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, posA);
                isDrawing = true;
                if (isDrawing)
                {
                    print(" drawing");
                    lineRenderer.SetPosition(1, posB);
                }

            }
        }
        if (!mouseDown)
        {
            isDrawing = false;
        }
    }
}
