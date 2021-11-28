using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour, ITarget
{
    //TODO: Change this to be a scriptableobject
    [Serializable]public struct EnemyStats
    {
        public int Damage;
        public float Speed;
        public float DamageRange;
        public float AttackRate;
        public int Health;
    }
    [SerializeField] private int Damage = 10;
    [SerializeField] private Transform Player;
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float RotationSpeed = 5.0f;
    [SerializeField] private float RotationSmoothTime = 0.8f;
    [SerializeField] private GameObject HitPs;
    [SerializeField] private GameObject BitePs;
    [SerializeField] private float DamageRange = 2.0f;
    [SerializeField] private float AttackRate = 5.0f;
    [SerializeField] private GameObject Body;
    private float _attackTime = 0.0f;
    private Coroutine _lookCoroutine;
    private Vector3 _rotationSmoothVelocity;
    private Vector3 _currentRotaion;
    private Vector3 _direction;
    private int _health = 100;
    private int _maxHealth = 100;
    private bool IsDead = false;
    private Rigidbody _rigidbody;
    private ITarget _playerTarget;
    static public Action OnDeath;
    static public Action OnHit;
    private void Update()
    {
        if(Player != null)
        {
            if (Vector3.Distance(Player.position, transform.position) < DamageRange && _attackTime == 0)
            {
                StartCoroutine(AttackCooldown());
                DealDamageToPlayer();
                LeanTween.moveLocalZ(Body, Body.transform.localPosition.z + 0.5f, 0.1f).setLoopPingPong(1).setDelay(0.05f);
                _rigidbody.velocity = Vector3.zero;
                //Damage player
                //Camera shake and things
            }
        }
        
    }

    private void DealDamageToPlayer()
    {
        _playerTarget.Damage(damage: Damage);
        Destroy(Instantiate(BitePs), 2f);
    }

    private IEnumerator AttackCooldown()
    {
        while(_attackTime < AttackRate)
        {
            _attackTime += Time.deltaTime;
            yield return null;
        }
        _attackTime = 0.0f;
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if(Player == null)
        {
            Player = FindObjectOfType<PlayerHealth>().transform;
            if (Player == null)
                return;
        }
        _playerTarget = Player.GetComponent<ITarget>();
    }
    private void FixedUpdate()
    {
        //Dont move when dead or attacking
        if(IsDead || _attackTime > 0 || Player == null)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _direction = Player.position - transform.position;
            _direction.y = 0.0f;
            _rigidbody.velocity = _direction.normalized * Speed;
        }
        
    }
    private void LateUpdate()
    {
        if(!IsDead)
        {
            Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction, Vector3.up), RotationSmoothTime);
            transform.rotation = smoothedRotation;
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
        if(IsDead)
        {
            return;
        }
        _health -= damage;
        OnHit?.Invoke();
        Destroy(Instantiate(HitPs,transform.position,Quaternion.identity), 2f);
        if(_health <= 0)
        {
            _health = _maxHealth;
            IsDead = true;
            _rigidbody.isKinematic = true;

            LeanTween.rotate(gameObject, new Vector3(0, 0, 180), 1f);
            Destroy(gameObject,2f);
            OnDeath?.Invoke();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DamageRange);
    }
#endif
    public void SetEnemyStats(EnemyStats stats)
    {
        Speed = stats.Speed;
        Damage = stats.Damage;
        DamageRange = stats.DamageRange;
        AttackRate = stats.AttackRate;
        _health = stats.Health;
    }

}
