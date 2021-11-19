using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject FirstGun;
    [SerializeField] private GameObject SecondGun;

    void Start()
    {
        FirstGun.SetActive(true);
        SecondGun.SetActive(false);
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
            }
            else
            {
                FirstGun.SetActive(true);
                SecondGun.SetActive(false);
            }
        }
    }
}
