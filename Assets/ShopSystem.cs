using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private Gun Weapon;
    [SerializeField] private CoinSystem CoinSystem;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private Transform ShopTransform;
    [SerializeField] private Transform Player;
    [SerializeField] private float Distance;
    [SerializeField] private Button AttackRateButton;
    [SerializeField] private Button DamageButton;
    [SerializeField] private Button AmmoButton;
    [SerializeField] private TMPro.TMP_Text DamageStatTxt;
    [SerializeField] private TMPro.TMP_Text AttackRateStatTxt;
    [SerializeField] private TMPro.TMP_Text MaxAmmoStatTxt;

    private bool CanBuyAttackRate = true;
    private MenuController _menuController;

    private void Awake()
    {
        _menuController = ShopPanel.GetComponent<MenuController>();
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (!ShopPanel.activeSelf &&Input.GetKeyDown(KeyCode.E) && Vector3.Distance(ShopTransform.position, Player.transform.position) < Distance)
        {
            _menuController.SetActive(true, 0.25f);
            ThirdPersonAim.FollowMouse = false;
            UpdateButtons();
        }
        else if(ShopPanel.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            _menuController.SetActive(false, 0.25f);
            ThirdPersonAim.FollowMouse = true;
        }
    }

    private void UpdateButtons()
    {
        DamageStatTxt.text = Weapon.DamageNumber.ToString();
        AttackRateStatTxt.text = (1 / Weapon.AttackRate).ToString("F2");
        MaxAmmoStatTxt.text = Weapon.AmmoCapacity.ToString();
        DisableButton(AmmoButton);
        DisableButton(DamageButton);
        DisableButton(AttackRateButton);
        if (CoinSystem.HasCoins(10))
        {
            EnableButton(AmmoButton);
            EnableButton(DamageButton);
            if (CanBuyAttackRate)
            {
                EnableButton(AttackRateButton);
            }
        }
    }

    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }
    public void BuyDamage()
    {
        if (CoinSystem.HasCoins(10))
        {
            Weapon.DamageNumber += 5;
            CoinSystem.RemoveCoins(10);
        }
        UpdateButtons();
    }
    public void BuyAmmoMax()
    {
        if (CoinSystem.HasCoins(10))
        {
            Weapon.AmmoCapacity += 5;
            CoinSystem.RemoveCoins(10);
        }
        UpdateButtons();
    }
    public void BuyAttackRate()
    {
        if (!CanBuyAttackRate) return;
        
        if(CoinSystem.HasCoins(10))
        {
            Weapon.AttackRate -= 0.02f;
            CoinSystem.RemoveCoins(10);
            if (Weapon.AttackRate < 0.1f)
            {
                Weapon.AttackRate = 0.1f;
                CanBuyAttackRate = false;
            }
        }
        UpdateButtons();
    }

    private void DisableButton(Button button)
    {
        Color defaultColor = button.image.color;
        button.image.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.5f);
        button.interactable = false;
    }
    private void EnableButton(Button button)
    {
        Color defaultColor = button.image.color;
        button.image.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1f);
        button.interactable = true;
    }
}
