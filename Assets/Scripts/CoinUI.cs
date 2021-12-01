using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    private TMPro.TMP_Text _text;
    [SerializeField] private GameObject AmmoAddedGO;
    private void Awake()
    {
        CoinSystem.OnAddedCoin += UpdateCoinText;
        _text = GetComponent<TMPro.TMP_Text>();
    }
    private void UpdateCoinText(int coin)
    {
        
        _text.text = coin.ToString();
        if (coin == 0)
            return;
        GameObject ammoText = Instantiate(AmmoAddedGO);
        ammoText.GetComponent<AnimatedText>().Text = "+1";
        ammoText.transform.SetParent(transform);
        ammoText.transform.localPosition = Vector3.zero;
    }
}
