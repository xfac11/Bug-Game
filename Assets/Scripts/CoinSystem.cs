using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    private int _coins;
    static public Action<int> OnAddedCoin;
    private void Awake()
    {
        AIEnemy.OnDeath += AddWorth;
    }

    private void AddWorth()
    {
        _coins++;
        OnAddedCoin?.Invoke(_coins);
    }
}
