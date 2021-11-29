using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckToGround : MonoBehaviour
{
    public LayerMask LayerMask;
    public bool UseGround = false;
    public Transform Ground;
    private Transform _transform;
    private float GroundY;
    private void Awake()
    {
        _transform = transform;
        if(UseGround)
            GroundY = Ground.position.y;
    }
    void FixedUpdate()
    {
        Vector3 myPos = _transform.position;
        if(UseGround)
        {
            _transform.position = new Vector3(myPos.x, GroundY, myPos.z);
            return;
        }
        Vector3 position = myPos;
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(position+new Vector3(0,1,0), direction, out RaycastHit hit, 5f, LayerMask))
        {
            _transform.position = new Vector3(position.x, hit.point.y, position.z);
        }
    }   
}
