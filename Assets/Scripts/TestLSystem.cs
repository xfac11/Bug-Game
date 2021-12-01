using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug.L_System;
public class TestLSystem : MonoBehaviour
{
    private LSystem _lSystem = new LSystem();
    // Start is called before the first frame update
    void Start()
    {
        _lSystem.AddConstant('-');
        _lSystem.AddRule('F', "F--F--F--G");
        _lSystem.AddRule('G', "GG");
        _lSystem.SetAxiom("F--F--F");
        Debug.Log(_lSystem.Generate(2));
    }
}
