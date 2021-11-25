using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject FirstGun;
    [SerializeField] private GameObject SecondGun;
    public Action<GameObject> GunSwitch;
    void Start()
    {
        SecondGun.GetComponent<Gun>().Shooting += CameraShaking; 
        FirstGun.SetActive(true);
        SecondGun.SetActive(false);
    }

    private void CameraShaking()
    {
        GetComponent<ThirdPersonAim>().CameraShake(0.2f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(FirstGun.activeSelf)
            {
                FirstGun.SetActive(false);
                SecondGun.SetActive(true);
                GunSwitch?.Invoke(SecondGun);
            }
            else
            {
                FirstGun.SetActive(true);
                SecondGun.SetActive(false);
                GunSwitch?.Invoke(FirstGun);
            }
        }
    }
}
