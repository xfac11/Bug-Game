using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEditor : MonoBehaviour
{
    [SerializeField] private Vector3 DirectionDis = new Vector3(2.54f, 1f, 1f);
    [SerializeField] private Vector2 MaxMin = new Vector2(0.5f, 1.5f);

    private static readonly string _DirectionDisStr = "Vector3_DirectionDis";
    private static readonly string _MaxMinDistStr = "Vector2_MaxMinDist";

    static readonly int shPropDirection = Shader.PropertyToID(_DirectionDisStr);
    static readonly int shPropMaxMin = Shader.PropertyToID(_MaxMinDistStr);

    private MaterialPropertyBlock _mbp;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
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
        Mbp.SetVector(shPropDirection, DirectionDis);
        _meshRenderer.SetPropertyBlock(Mbp);
    }

    void ApplyMaxMin()
    {
        Mbp.SetVector(shPropMaxMin, MaxMin);
        _meshRenderer.SetPropertyBlock(Mbp);
    }

    private void OnValidate()
    {
        if (_meshRenderer == null)
            _meshRenderer = GetComponent<MeshRenderer>();
        ApplyDirection();
        ApplyMaxMin();
    }
}
