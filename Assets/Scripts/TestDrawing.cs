using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrawing : MonoBehaviour
{
    public LineRenderer line; // Assign via inspector
    bool isDrawing = false;

    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            
            line.SetPosition(1, pos);
        }
    }
}
