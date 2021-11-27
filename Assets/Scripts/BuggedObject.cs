using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuggedObject : MonoBehaviour,ITarget
{
    [SerializeField] private DissolveEditor DissolveEditor;
    [SerializeField] private GameObject OneZeroParticlesystem;
    [SerializeField] private GameObject FixedParticles;
    public event Action<GameObject> Fixed;

    private int _health = 100;
    [SerializeField]private int Maxhealth = 100;
    private Vector2 _defaultMaxMin;
    private bool _fixed = false;
    public void Damage(int damage)
    {
        if (_fixed)
            return;
        _health -= damage;
        if (_health < 0)
            _health = 0;

        float healthf = _health;

        float newMax = Mathf.Lerp(1f, _defaultMaxMin.x, healthf / Maxhealth);
        float newMin = Mathf.Lerp(1f, _defaultMaxMin.y, healthf / Maxhealth);
        DissolveEditor.MaxMinPosition = new Vector2(newMax, newMin);
        if (_health == 0)
            BugFixed();
    }

    private void BugFixed()
    {
        //Remove 1's and 0's particles.
        OneZeroParticlesystem.SetActive(false);
        //Instantiate "fixed" particles
        //Replace with pooling
        GameObject clone = Instantiate(FixedParticles, gameObject.transform);
        Destroy(clone, 4f);
        //Invoke fixed event
        //Use to scoring, ammo etc.
        Fixed?.Invoke(gameObject);
        _fixed = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultMaxMin = DissolveEditor.MaxMinPosition;
        _health = Maxhealth;
    }
}
