using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera TheCamera;
    public LayerMask LayerMask;
    public float Range;

    public ParticleSystem MussleParticles;
    public GameObject HitEffect;//Change this to be in the Target so we can have different HitEffects for each target.
                                //Or maybe this is the guns HitEffect and in target theirs an HurtEffect

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if(ShootRay(out hit))
            {
                //Damage and VFX on the damaged thing. Particles etc.
                Damage(hit);
            }
            MussleFlare();

        }
    }

    private void Damage(RaycastHit hit)
    {
        GameObject ps = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(ps, 2f);
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
