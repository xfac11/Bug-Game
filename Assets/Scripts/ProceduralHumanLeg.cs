using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralHumanLeg : MonoBehaviour
{
    [SerializeField] private GameObject TheEllipsoid;
    [SerializeField] private GameObject LegTarget;
    [SerializeField] private GameObject Foot;
    [SerializeField] private GameObject DefaultFoot;
    private Vector3 _footPosition;
    public Vector3 FootPositon
    {
        get
        {
            if (Foot == null)
                return Vector3.zero;
            return Foot.transform.position;
        }
    }
    private Ellipsoid _ellipsoid;
    private float _angleCollision = 0.0f;
    private bool _followEllipsoid = true;
    private Vector3 _stuckPositon;
    public Vector3 StuckPosition
    {
        set
        {
            _stuckPositon = value;
        }
    }
    public bool FollowEllipsoid
    {
        get
        {
            return _followEllipsoid;
        }
        set
        {
            _followEllipsoid = value;
        }
    }
    public GameObject Target
    {
        get
        {
            return LegTarget;
        }
        set
        {
            LegTarget = value;
        }
    }
    public Ellipsoid Ellipsoid
    {
        get
        {
            return _ellipsoid;
        }
    }


    private void Awake()
    {
        _ellipsoid = TheEllipsoid.GetComponent<Ellipsoid>();
    }

    private void Update()
    {
        if(_followEllipsoid)
        {
            LegTarget.transform.position = _ellipsoid.GetRotatedObject().transform.position;
            Ray ray = new Ray(FootPositon, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            {
                if (hit.point.y > _ellipsoid.GetRotatedObject().transform.position.y)
                {
                    LegTarget.transform.position = new Vector3(LegTarget.transform.position.x, hit.point.y, LegTarget.transform.position.z);
                }
            }
        }
        else
        {
            LeanTween.move(LegTarget, DefaultFoot.transform.position, 0.1f);
        }
    }


}
