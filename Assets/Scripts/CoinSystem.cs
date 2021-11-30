using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    private int _coins = 99;
    static public Action<int> OnAddedCoin;
    private void Awake()
    {
        AIEnemy.OnDeath += AddWorth;
    }
    private void Start()
    {
        AddWorth();
    }
    private void AddWorth()
    {
        _coins++;
        OnAddedCoin?.Invoke(_coins);
    }
    public bool HasCoins(int coins)
    {
        return _coins >= coins;
    }
    public bool RemoveCoins(int coins)
    {
        if(coins > _coins) return false;

        _coins -= coins;
        OnAddedCoin?.Invoke(_coins);
        return true;
    }
}
