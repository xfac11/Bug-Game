using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int MaxAmmo = 20;
    public Camera TheCamera;
    public LayerMask LayerMask;
    public float Range = 20.0f;
    public float AttackRate = 2.0f;
    public int DamageNumber = 20;
    public ParticleSystem MussleParticles;
    public bool IsSpray = false;//TODO:Make spray
    public GameObject HitEffect;//TODO:Change this to be in the Target so we can have different HitEffects for each target.
    public GameObject GunTrail;
    public bool UseAmmo = false;
    private float _time = 0.0f;
    private bool _spraying = false;
    private int _ammo;
    public bool DamageEnemies = true;
    public int Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
            if (_ammo > MaxAmmo)
                _ammo = MaxAmmo;
        }
    }
    public int AmmoCapacity
    {
        get
        {
            return MaxAmmo;
        }
    }

    private void Start()
    {
        _ammo = MaxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        //This needs to be handled by animation. Holding the gun and pointing at the direction of the cameras forward.
        gameObject.transform.forward = TheCamera.transform.forward;
        if(Input.GetMouseButton(0) && (_ammo > 0 || !UseAmmo))
        {
            if(IsSpray && !_spraying)
            {
                _spraying = true;
                MussleFlare();
            }
            _time += Time.deltaTime;
            if(_time >= AttackRate || Input.GetMouseButtonDown(0))
            {
                LineRenderer lr;
                if (GunTrail != null)
                {
                    lr = Instantiate(GunTrail).GetComponent<LineRenderer>();
                    Destroy(lr.gameObject, 0.1f);
                    lr.SetPosition(0, transform.position);
                    lr.SetPosition(1, transform.position+transform.forward * 100);
                }
                if(UseAmmo)
                    _ammo -= 1;
                if (ShootRay(out RaycastHit hit))
                {
                    //Damage and VFX on the damaged thing. Particles etc.
                    DamageTarget(hit);
                }
                if (!IsSpray)
                    MussleFlare();
                _time = 0.0f;

            }
        }
        else
        {
            _spraying = false;
            MussleParticles.Stop();
        }
    }

    private void DamageTarget(RaycastHit hit)
    {
        GameObject ps = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(ps, 2f);

        if(DamageEnemies)
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                ITarget target = hit.collider.GetComponent<ITarget>();
                if (target != null)
                {
                    target.Damage(DamageNumber);
                }
            }
        }
        else
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            if (target != null)
            {
                target.Damage(DamageNumber);
            }
        }
    }

    private void MussleFlare()
    {
        MussleParticles.Play();
    }

    private bool ShootRay(out RaycastHit hit)
    {
        return Physics.Raycast(TheCamera.transform.position, TheCamera.transform.forward, out hit, Range, LayerMask);
    }
}
