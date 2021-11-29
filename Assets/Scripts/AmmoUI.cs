using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private GunSwitcher GunSwitcher;
    private GameObject _gunObj;
    private Gun _gun;
    private TMP_Text _text;
    [SerializeField] private Image GunImage;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        GunSwitcher.GunSwitch += UpdateAmmoText;
    }

    private void UpdateAmmoText(GameObject obj)
    {
        _gunObj = obj;
        _gun = obj.GetComponent<Gun>();
        GunImage.sprite = _gun.GetImage();
    }
    private void Update()
    {
        if (_text == null || _gunObj == null)
            return;
        string ammotext = _gun.Ammo + "/" + _gun.AmmoCapacity;
        if (!_gun.UseAmmo)
            ammotext = "Infinity";
        _text.text = ammotext;
    }
}
