using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private GunSwitcher GunSwitcher;
    private GameObject _gun;
    private TMP_Text Text;
    [SerializeField] Image GunImage;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TMP_Text>();
        GunSwitcher.GunSwitch += UpdateAmmoText;
    }

    private void UpdateAmmoText(GameObject obj)
    {
        _gun = obj;
        GunImage.sprite = obj.GetComponent<Gun>().GetImage();
    }
    private void Update()
    {
        if (Text == null || _gun == null)
            return;
        string ammotext = _gun.GetComponent<Gun>().Ammo + "/" + _gun.GetComponent<Gun>().AmmoCapacity;
        if (!_gun.GetComponent<Gun>().UseAmmo)
            ammotext = "Infinity";
        Text.text = ammotext;
    }
}
