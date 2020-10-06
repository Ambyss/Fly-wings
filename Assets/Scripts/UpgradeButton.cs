using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : Upgrades
{
    private Text _upgradeText;
    private Text _costText;
    private int _cost;
    private UpgradeSystem _upgradeSystem;
    
    
    private void Start()
    {
        _upgradeSystem = transform.parent.GetComponent<UpgradeSystem>();
        InitName();
        _upgradeText = transform.GetChild(0).GetComponent<Text>();
        _costText = transform.GetChild(1).GetComponent<Text>();
        StartCoroutine(StartInit());
    }

    IEnumerator StartInit()
    {
        yield return new WaitForSecondsRealtime(.01f);
        UpdateData();
    }
    
    private void InitName()
    {
        switch (name)
        {
            case "Tail":
                upgradeName = Name.Tail;
                break;
            case "Wing":
                upgradeName = Name.Wing;
                break;
            case "Engine":
                upgradeName = Name.Engine;
                break;
            case "Jump":
                upgradeName = Name.Jump;
                break;
            case "Money":
                upgradeName = Name.Money;
                break;
            case "Speed":
                upgradeName = Name.Speed;
                break;
            case "Boost":
                upgradeName = Name.Boost;
                break;
            case "Fuel":
                upgradeName = Name.Fuel;
                break;
        }
    }

    private void ChangeUpgradeShow()
    {
        _upgradeText.text = currentUpgrade + " / " + maxUpgrades;
        _costText.text = _cost.ToString();
    }


    public void UpdateData()
    {
        _upgradeSystem.GiveMeInfo(upgradeName, out currentUpgrade, out maxUpgrades, out _cost);
        ChangeUpgradeShow();
    }

    public void OnClick()
    {
        if (_upgradeSystem.IsEnoughMoney(_cost))
            _upgradeSystem.Upgrade(upgradeName);
    }
}