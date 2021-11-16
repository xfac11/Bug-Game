using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    public List<GameObject> Legs;
    public Vector3 BodyOffset = new Vector3(0.1f, 0.5f, 0.1f);

    private Vector3 _averagePos = Vector3.zero;
    private bool _moving = false;
    private Vector3 _oldPos;
    private float time;
    public float MaxTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!_moving)
        {
            _averagePos = GetAverageVector(Legs);
            if (Vector3.Distance(_averagePos + BodyOffset, transform.position) > 1.0f)
            {
                _moving = true;
                _oldPos = transform.position;
            }
        }
        else
        {
            Vector3 newPosition = Vector3.Lerp(_oldPos, _averagePos + BodyOffset, time / MaxTime);
            transform.position = newPosition;
            time += Time.deltaTime;
            if (time >= MaxTime)
            {
                _moving = false;
                time = 0.0f;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_averagePos, 0.1f);
    }

    private Vector3 GetAverageVector(List<GameObject> gameObjects)
    {
        if (gameObjects.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 meanVector = Vector3.zero;

        foreach (GameObject obj in gameObjects)
        {
            meanVector += obj.transform.position;
        }

        return (meanVector / gameObjects.Count);
    }
}
