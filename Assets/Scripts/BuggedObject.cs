using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuggedObject : MonoBehaviour,IITarget
{
    [SerializeField] private DissolveEditor DissolveEditor;
    [SerializeField] private GameObject OneZeroParticlesystem;
    [SerializeField] private GameObject FixedParticles;
    public event Action<GameObject> Fixed;

    private int _health = 100;
    private int _maxhealth = 100;
    private Vector2 _defaultMaxMin;
    public void Damage(int damage)
    {
        _health -= damage;
        if (_health < 0)
            _health = 0;
        float rateX = _maxhealth / _defaultMaxMin.x;
        float rateY = _maxhealth / _defaultMaxMin.y;
        Mathf.Lerp(1f, _defaultMaxMin.x, _health / _maxhealth);
        Mathf.Lerp(1f, _defaultMaxMin.y, _health / _maxhealth);
        DissolveEditor.MaxMinPosition -= new Vector2(damage * rateX, damage * rateY);
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
        Destroy(clone, 2f);
        //Invoke fixed event
        //Use to scoring, ammo etc.
        Fixed?.Invoke(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultMaxMin = DissolveEditor.MaxMinPosition;
    }
}
