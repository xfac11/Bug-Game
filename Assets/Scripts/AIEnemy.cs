using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        public EnemyStats IncreasePositive(float damagePer, float damageRangePer, float speedPer, float attackRatePer, float healthPer)
        {

            Damage = (int)(Damage * damagePer);
            DamageRange *= damageRangePer;
            AttackRate *= attackRatePer;
            Health = (int)(Health * healthPer);
            Speed *= speedPer;
            
            return this;

        }
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
    private ITarget _playerTarget;
    private NavMeshAgent _navMeshAgent;
    static public Action OnDeath;
    static public Action OnHit;
    private void Update()
    {
        if(Player != null)
        {
            if (Vector3.Distance(Player.position, transform.position) < DamageRange && _attackTime == 0 && !IsDead)
            {
                StartCoroutine(AttackCooldown());
                DealDamageToPlayer();
                LeanTween.moveLocalZ(Body, Body.transform.localPosition.z + 0.5f, 0.1f).setLoopPingPong(1).setDelay(0.05f);
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
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            _navMeshAgent.isStopped = true;
        }
        else
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = Player.position;
            _navMeshAgent.speed = Speed;
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
            LeanTween.rotate(gameObject, new Vector3(0, 0, 170), 1f);
            Destroy(gameObject,1f);
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
