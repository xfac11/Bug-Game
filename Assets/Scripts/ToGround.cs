using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGround : MonoBehaviour
{

    /// <summary>
    /// Need to improve this. When it gets to fra away it just keeps going to the gorundpoint and then back to the previous position and so forth
    /// </summary>
    public Transform Ground;
    public GameObject OppositeLeg;
    private ToGround _oppositeLeg;
    public bool UseNearestGroundPoint;
    public LayerMask LayerMask;
    private Vector3 stuckPosition;
    public float Distance = 5.0f;
    private bool _shouldStuck = true;
    private float time;
    public float StepHeight = 1.0f;
    public Transform GroundedPoint;
    private Vector3 oldPos;
    private Vector3 oldGrounded;

    public float MaxTime = 1.0f;
    public bool ShouldStuck
    {
        get
        {
            return _shouldStuck;
        }
    }
    void Start()
    {
        _oppositeLeg = OppositeLeg.GetComponent<ToGround>();
        Vector3 position = transform.position;
        Vector3 direction = Vector3.down;
        if(UseNearestGroundPoint)
        {
            if(Physics.Raycast(position, direction,out RaycastHit hit, 100.0f, LayerMask))
            {
                stuckPosition = hit.point;
            }
            Debug.Log("no hit");
        }
        else
        {
            stuckPosition = new Vector3(transform.position.x, Ground.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_shouldStuck)
        {
            if (UseNearestGroundPoint)
            {
                transform.position = stuckPosition;
            }
            else
            {
                Vector3 vector = new Vector3(transform.position.x, Ground.position.y, transform.position.z);
                transform.position = vector;
            }
            if (Vector3.Distance(transform.position, GroundedPoint.position) >= Distance && _oppositeLeg.ShouldStuck)
            {
                _shouldStuck = false;
                oldPos = transform.position;
                oldGrounded = GroundedPoint.position;
                //Move to GroundedPoint
            }
        }
        else
        {
            Vector3 newPosition = Vector3.Lerp(oldPos, GroundedPoint.position, time/MaxTime);
            newPosition.y += Mathf.PingPong(2 * time / MaxTime, 1) * StepHeight;
            transform.position = newPosition;
            time += Time.deltaTime;
            if(time >= MaxTime)
            {
                _shouldStuck = true;
                time = 0.0f;
                stuckPosition = newPosition;
            }
        }
    }
}
