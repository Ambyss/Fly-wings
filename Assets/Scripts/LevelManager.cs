using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int _boostPower;
    private int _moneyCost;
    private float _boostFrequency;
    private float _moneyFrequency;
    private int _money;

    private void Awake()
    {
        TemporaryInitializationDeleteMeBeforeBuildPls();
    }

    private void TemporaryInitializationDeleteMeBeforeBuildPls()
    {
        _boostPower = 550;
        _moneyCost = 1;
        _boostFrequency = 10;
        _moneyFrequency = 10;
        _money = 5000;
    }

    public int GetBoostPower()
    {
        return _boostPower;
    }

    public void GameEnds(bool isWin)
    {
        if (isWin)
            Debug.Log("End Game");
        else 
            Debug.Log("Crash");
    }
}
