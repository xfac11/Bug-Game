using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour,ITarget
{
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] private WaveHandler WaveHandler;
    private int _health = 100;
    private Bug.Controls.ThirdPersonMovement _thirdPersonMovement;
    public UnityEvent PlayerDead;
    private ThirdPersonAim _thirdPersonAim;
    
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
            PlayerDead?.Invoke();
            Destroy(gameObject, 1f);
        }
    }

    private void DamageHit()
    {
        
    }

    private void CameraShake()
    {
        _thirdPersonAim.CameraShake(0.3f, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
        _thirdPersonMovement = GetComponent<Bug.Controls.ThirdPersonMovement>();
        _thirdPersonAim = GetComponent<ThirdPersonAim>();
        WaveHandler.OnWaveFinished += ReplenishHealth;
    }

    private void ReplenishHealth()
    {
        StartCoroutine(LerpHealth(1f));
    }
    private IEnumerator LerpHealth(float maxTime)
    {
        float t = 0;
        float time = 0;
        float currentHealth = _health;
        while(time < maxTime)
        {
            time += Time.deltaTime;
            _health = (int)Mathf.Lerp(currentHealth, MaxHealth, time / maxTime);
            yield return null;
        }
        if(_health > MaxHealth)
        {
            _health = MaxHealth;
        }
    }
}
