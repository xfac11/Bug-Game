using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    private TMPro.TMP_Text _text;
    private void Awake()
    {
        CoinSystem.OnAddedCoin += UpdateCoinText;
        _text = GetComponent<TMPro.TMP_Text>();
    }
    private void UpdateCoinText(int coin)
    {
        _text.text = coin.ToString();
    }
}
