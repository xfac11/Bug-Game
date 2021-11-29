using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject FirstGun;
    [SerializeField] private GameObject SecondGun;
    private GameObject _currentWeapon;
    private int _gunNumber;
    public Action<GameObject> GunSwitch;
    private ThirdPersonAim _thirdPersonAim;
    void Start()
    {
        SecondGun.GetComponent<Gun>().Shooting += CameraShaking; 
        FirstGun.SetActive(false);
        SecondGun.SetActive(false);
        _currentWeapon = FirstGun;
        _gunNumber = 1;
        _thirdPersonAim = GetComponent<ThirdPersonAim>();
    }

    private void CameraShaking()
    {
        _thirdPersonAim.CameraShake(0.2f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(_gunNumber  == 1)
            {
                _gunNumber = 2;
                _currentWeapon = SecondGun;
                GunSwitch?.Invoke(_currentWeapon);

            }
            else if(_gunNumber == 2)
            {
                _gunNumber = 1;
                _currentWeapon = FirstGun;
                GunSwitch?.Invoke(_currentWeapon);

            }
        }
        if(Input.GetMouseButtonDown(1))
        {
        }
        if (Input.GetMouseButton(1))
        {
            _currentWeapon.SetActive(true);
            if(_gunNumber == 1)
            {
                SecondGun.SetActive(false);
            }
            else if(_gunNumber == 2)
            {
                FirstGun.SetActive(false);
            }
        }
        else
        {
            FirstGun.SetActive(false);
            SecondGun.SetActive(false);
        }
    }
}
