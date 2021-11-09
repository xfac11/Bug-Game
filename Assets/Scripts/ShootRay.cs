using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour
{
    public Camera currentCamera;
    public LayerMask layer;
    public GameObject hitBall;

    private void FixedUpdate()
    {
        Vector3 direction = currentCamera.transform.forward;
        Vector3 origin = currentCamera.transform.position;
        RaycastHit hit;
        if(Physics.Raycast(origin, direction, out hit, Mathf.Infinity, layer))
        {
            Vector3 hitPosition = hit.point;
            hitBall.transform.position = hitPosition;
        }
    }
}
