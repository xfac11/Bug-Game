using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipsoid : MonoBehaviour
{
    public float StartAngle = 0f;
    public float a = 1f; //a is in AU, Semimajor Axis
    public float b = 2f;
    public float angle; // angle theta
    public float speed = (2 * Mathf.PI) / 10;
    public bool UseSpeed = true;
    private float x;
    private float y;
    [SerializeField] private GameObject RotatedObject;
    private GameObject _rotatedObject;
    public void SetRotatedObject(GameObject gameObject)
    {
        _rotatedObject = gameObject;
    }
    public GameObject GetRotatedObject()
    {
        return _rotatedObject;
    }
    private void Start()
    {
        _rotatedObject = RotatedObject;
        angle = Mathf.Deg2Rad*StartAngle;
    }
    // Update is called once per frame
    void Update()
    {
        if(_rotatedObject == null)
        {
            return;
        }
        if(UseSpeed)
        {
            angle += speed * Time.deltaTime;
        }
        x = Mathf.Cos(angle) * a; // a is the Radius in the x direction
        y = Mathf.Sin(angle) * b; // b is the  Radius in the y direction
        _rotatedObject.transform.localPosition = new Vector3(0, x, y);
    }
    private void OnValidate()
    {
        _rotatedObject = RotatedObject;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, a, 0), 0.2f);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0, b), 0.2f);
        Gizmos.color = Color.white;
        DrawAround(30);
    }

    private void DrawGizmoLine(float angle)
    {
        float radians = Mathf.Deg2Rad * angle;
        float x = Mathf.Cos(radians) * a; // a is the Radius in the x direction
        float y = Mathf.Sin(radians) * b; // b is the  Radius in the y direction
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, x, y));
    }
    private void DrawAround(float angle)
    {
        int length = 360 / (int)angle;
        float startAngle = 0;
        for (int i = 0; i < length; i++)
        {
            DrawGizmoLine(startAngle);
            startAngle += angle;
        }
    }
}