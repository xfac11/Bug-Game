using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralHumanAnimation : MonoBehaviour
{
    [SerializeField] private ProceduralHumanLeg L_Leg;
    [SerializeField] private ProceduralHumanLeg R_Leg;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject L_HandTarget;
    [SerializeField] private GameObject R_HandTarget;
    private Vector3 L_HandTargetDefault;
    private Vector3 R_HandTargetDefault;
    private GameObject L_HandTargetFollow;
    private GameObject R_HandTargetFollow;
    public Vector3 Velocity;
    public float Speed;
    public float SpeedMultiplier = 1.0f;
    private Vector3 previousPosition;
    private Vector3 currentPositon;
    private float pingPongValue = 0;
    private Queue<ProceduralHumanLeg> legQueue;
    private ProceduralHumanLeg _currentMovingLeg;
    private float elapsedTime;
    [SerializeField] private GunSwitcher GunSwitcher;
    private void Awake()
    {
        GunSwitcher.GunSwitch += ChangeHands;
        L_HandTargetDefault = L_HandTarget.transform.localPosition;
        R_HandTargetDefault = R_HandTarget.transform.localPosition;
        previousPosition = transform.position;
        currentPositon = transform.position;
        legQueue = new Queue<ProceduralHumanLeg>();
        legQueue.Enqueue(L_Leg);
        legQueue.Enqueue(R_Leg);
        
    }

    private void ChangeHands(GameObject obj)
    {
        L_HandTargetFollow = obj.GetComponent<Gun>().LeftHandPosition;
        R_HandTargetFollow = obj.GetComponent<Gun>().RightHandPosition;
    }

    private void Start()
    {
        L_Leg.Ellipsoid.UseSpeed = true;
        R_Leg.Ellipsoid.UseSpeed = true;
    }
    private void FixedUpdate()
    {
        
        float time = Time.fixedDeltaTime;
        currentPositon = transform.position;
        float distance = Vector3.Distance(currentPositon, previousPosition);
        //S = V*T
        //V = S/T
        Speed = distance / time;
        Velocity = (currentPositon - previousPosition) / time;
        previousPosition = currentPositon;
    }
    private void Update()
    {
        if(L_HandTargetFollow != null && L_HandTargetFollow.activeInHierarchy)
        {
            L_HandTarget.transform.position = L_HandTargetFollow.transform.position;
            R_HandTarget.transform.position = R_HandTargetFollow.transform.position;
        }
        else
        {
            AnimateSine(L_HandTarget, L_HandTargetDefault, new Vector3(0f, 0.5f, 1f), 1f, 0.1f);
            AnimateSine(R_HandTarget, R_HandTargetDefault, new Vector3(0f, 0.5f, 1f), 1f, 0.1f);
        }
        elapsedTime += Time.deltaTime;
        L_Leg.Ellipsoid.speed = -Speed*SpeedMultiplier;
        R_Leg.Ellipsoid.speed = -Speed*SpeedMultiplier;
        if(Speed == 0)
        {
            L_Leg.FollowEllipsoid = false;
            R_Leg.FollowEllipsoid = false;
        }
        else
        {
            L_Leg.FollowEllipsoid = true;
            R_Leg.FollowEllipsoid = true;
        }
        body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + body.transform.position.y * Time.deltaTime*Speed * Mathf.Sin(Time.time*10.0f)*0.01f, body.transform.position.z);
        //if(Speed == 0)
        //{//Idle
        //    AnimateSine(L_HandTarget, L_HandTargetDefault, new Vector3(0f, 0.5f, 1f), 1f, 0.1f);
        //    AnimateSine(R_HandTarget, R_HandTargetDefault, new Vector3(0f, 0.5f, 1f), 1f, 0.1f);
        //}
        //else
        //{//Walk
        //    AnimateSine(L_HandTarget, L_HandTargetDefault, new Vector3(0f, 0.5f, 1f), 10f, 0.5f);
        //    AnimateSine(R_HandTarget, R_HandTargetDefault, new Vector3(0f, -0.5f, -1f), 10f, 0.5f);
        //}
    }

    private bool IsCloseToGround(out RaycastHit hitInfo, Vector3 position)
    {
        RaycastHit raycastHit = new RaycastHit();
        hitInfo = raycastHit;
        Ray ray = new Ray(position, Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, 0.3f))
        {
            hitInfo = hit;
            return true;
        }
        return false;
    }
    private void AnimateSine(GameObject gameObject,Vector3 originalPosition, Vector3 amount, float speed, float scale)
    {
        float sine = Mathf.Sin(elapsedTime* speed) * scale;
        gameObject.transform.localPosition = new Vector3(
            originalPosition.x + amount.x * sine,
            originalPosition.y + amount.y * sine,
            originalPosition.z + amount.z * sine
            );;
    }
}
