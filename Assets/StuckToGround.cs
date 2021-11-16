using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckToGround : MonoBehaviour
{
    public LayerMask LayerMask;
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(position+new Vector3(0,1,0), direction, out RaycastHit hit, 100.0f, LayerMask))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
        
}
