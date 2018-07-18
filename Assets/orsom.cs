using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orsom : MonoBehaviour
{
    public int gridHeight;
    public int gridWidth;
    public Vector3 startPosition;
    public bool mouseDown;
    public bool isDrawing;
    public bool lineRendererInited;
    public GameObject lineRendererPF;
    public int[] grid;
    public GameObject pointPf;
    // Use this for initialization
    private static orsom _instance;

    public static orsom Instance { get { return _instance; } }


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
    void Start()
    {
        grid = new int[gridHeight * gridWidth];
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                GameObject obj = Instantiate(pointPf, startPosition + new Vector3(i, j, 0), Quaternion.identity);
                if (i >= (int)gridWidth / 2)
                {
                    obj.AddComponent<CircleCollider2D>().radius = 0.2f;
                }
            }
        }
    }
    public void Orsomli5at(Vector3 posA, Vector3 posB)
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
