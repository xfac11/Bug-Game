using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug.L_System;
public class PlusInstruction : MonoBehaviour, IInstruction
{
    [SerializeField] private float Angle;
    private char _symbol;
    public char Symbol { get => '+'; set => _symbol = value; }

    public void Operation()
    {
        transform.Rotate(new Vector3(0, -Angle, 0));
    }
}
