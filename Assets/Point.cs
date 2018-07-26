using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    //=================================================================
    private void OnMouseDown()
    {
        orsom.totalClicks++;
        orsom.selectedPoints.Add(this);
        //print(this);
    }

    public override string ToString()
    {
        return "("+this.transform.position+")";
    }
}


