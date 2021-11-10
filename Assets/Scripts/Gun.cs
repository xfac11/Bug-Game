using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera TheCamera;
    public LayerMask LayerMask;
    public float Range = 20.0f;
    public float AttackRate = 2.0f;
    public int DamageNumber = 20;
    public ParticleSystem MussleParticles;
    public bool IsSpray = false;
    public GameObject HitEffect;//Change this to be in the Target so we can have different HitEffects for each target.
                                //Or maybe this is the guns HitEffect and in target theirs an HurtEffect
    private float _time = 0.0f;
    private bool _spraying = false;
    // Update is called once per frame
    void Update()
    {
        //This needs to be handled by animation. Holding the gun and pointing at the direction of the cameras forward.
        gameObject.transform.forward = TheCamera.transform.forward;
        if(Input.GetMouseButton(0))
        {
            if(IsSpray && !_spraying)
            {
                _spraying = true;
                MussleFlare();
            }
            _time += Time.deltaTime;
            if(_time >= AttackRate || Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (ShootRay(out hit))
                {
                    //Damage and VFX on the damaged thing. Particles etc.
                    DamageTarget(hit);
                }
                if(!IsSpray)
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

        ITarget target = hit.collider.GetComponent<ITarget>();
        if(target != null)
        {
            target.Damage(DamageNumber);
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
