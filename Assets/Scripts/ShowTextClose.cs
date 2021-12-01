using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextClose : MonoBehaviour
{
    [SerializeField] private Transform NearObject;
    [SerializeField] private Transform Player;
    [SerializeField] private float Distance;
    [SerializeField] private GameObject ToShow;
    private void Update()
    {
        if(Vector3.Distance(NearObject.position,Player.position) <= Distance)
        {
            ToShow.SetActive(true);
        }
        else
        {
            ToShow.SetActive(false);
        }
    }
}
