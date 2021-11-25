using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,ITarget
{
    [SerializeField] private int MaxHealth = 100;
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
    }
    public int HealthCapacity
    {
        get
        {
            return MaxHealth;
        }
    }
    public void Damage(int damage)
    {
        _health -= damage;
        CameraShake();
        DamageHit();
        if(_health <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

    private void DamageHit()
    {
        
    }

    private void CameraShake()
    {
        GetComponent<ThirdPersonAim>().CameraShake(0.1f, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
