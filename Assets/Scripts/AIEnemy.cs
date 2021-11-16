using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour, ITarget
{
    [SerializeField] private float Damage = 10.0f;
    [SerializeField] private Transform Player;
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float WalkRange = 5.0f;
    [SerializeField] private float RotationSpeed = 5.0f;
    [SerializeField] private float RotationSmoothTime = 1.0f;
    [SerializeField] private GameObject HitPs;
    private Coroutine _lookCoroutine;
    private Vector3 _rotationSmoothVelocity;
    private Vector3 _currentRotaion;
    private Vector3 _direction;
    private int _health = 100;
    private bool IsDead = false;
    private Rigidbody _rigidbody;


    private void Update()
    {
        if(Vector3.Distance(Player.position,transform.position)<2.0f)
        {
            //Damage player
            //Camera shake and things
        }
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _direction = Player.position - transform.position;
        _direction.y = 0.0f;
        _rigidbody.velocity = _direction.normalized * Speed;
        if(IsDead)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
    private void LateUpdate()
    {
        if(!IsDead)
        {
            _currentRotaion = Vector3.SmoothDamp(_currentRotaion, Quaternion.LookRotation(_direction, Vector3.up).eulerAngles, ref _rotationSmoothVelocity, RotationSmoothTime);
            transform.eulerAngles = _currentRotaion;
        }
    }
    public void StartRotating()
    {
        if(_lookCoroutine != null)
        {
            StopCoroutine(_lookCoroutine);
        }

        _lookCoroutine = StartCoroutine(LookAt());
    }

    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_direction - transform.position);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * RotationSpeed;

            yield return null;
        }
    }

    void ITarget.Damage(int damage)
    {
        _health -= damage;
        Destroy(Instantiate(HitPs,transform.position,Quaternion.identity), 2f);
        if(_health <= 0)
        {
            _health = 100;
            IsDead = true;
            _rigidbody.isKinematic = true;

            LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 1f);
            Destroy(gameObject,2f);
        }
    }
}
