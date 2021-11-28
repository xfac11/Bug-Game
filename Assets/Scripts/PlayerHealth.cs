using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,ITarget
{
    [SerializeField] private int MaxHealth = 100;
    private int _health = 100;
    private Bug.Controls.ThirdPersonMovement _thirdPersonMovement;
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
        _thirdPersonMovement.Stun(0.2f);
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
        GetComponent<ThirdPersonAim>().CameraShake(0.3f, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
        _thirdPersonMovement = GetComponent<Bug.Controls.ThirdPersonMovement>();
    }
}
