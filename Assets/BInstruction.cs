using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BInstruction : MonoBehaviour, Bug.L_System.IInstruction
{
    private char _symbol;
    public char Symbol { get => 'B'; set => _symbol = value; }
    public float Speed = 1.0f;
    public void Operation()
    {
        GetComponent<LineRenderer>().positionCount++;
        int index = GetComponent<LineRenderer>().positionCount - 1;
        transform.position += transform.forward * Speed;
        GetComponent<LineRenderer>().SetPosition(index, transform.position);
    }
}
