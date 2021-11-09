using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEditor : MonoBehaviour
{
    [SerializeField] private Vector3 DirectionDis;
    [SerializeField] private Vector2 MaxMin;

    private static readonly string _DirectionDisStr = "Vector3_DirectionDis";
    private static readonly string _MaxMinDistStr = "Vector2_MaxMinDist";

    static readonly int shPropDirection = Shader.PropertyToID(_DirectionDisStr);
    static readonly int shPropMaxMin = Shader.PropertyToID(_MaxMinDistStr);

    private MaterialPropertyBlock _mbp;
    public MaterialPropertyBlock Mbp
    {
        get
        {
            if (_mbp == null)
                _mbp = new MaterialPropertyBlock();
            return _mbp;
        }
    }

    /// <summary>
    /// Use this Propery to change the Disolve direction
    /// </summary>
    public Vector3 Direction
    {
        get
        {
            return DirectionDis;
        }
        set
        {
            DirectionDis = value;
            ApplyDirection();
        }
    }

    /// <summary>
    /// Use this Propery to change Max and Min values of the possible position
    /// </summary>
    public Vector2 MaxMinPosition
    {
        get
        {
            return MaxMin;
        }
        set
        {
            MaxMin = value;
            ApplyMaxMin();
        }
    }

    void ApplyDirection()
    {
        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mbp.SetVector(shPropDirection, DirectionDis);
        rnd.SetPropertyBlock(Mbp);
    }

    void ApplyMaxMin()
    {
        MeshRenderer rnd = GetComponent<MeshRenderer>();
        Mbp.SetVector(shPropMaxMin, MaxMin);
        rnd.SetPropertyBlock(Mbp);
    }

    private void OnValidate()
    {
        ApplyDirection();
        ApplyMaxMin();
    }
}
