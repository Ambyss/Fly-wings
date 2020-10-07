using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{ 
    public enum Name
    {
        Boost,
        Jump,
        Speed,
        Money,
        Fuel,
        Engine,
        Wing,
        Tail
    };
    public Name upgradeName;
    public int currentUpgrade;
    public int maxUpgrades;
    public int startCost;
    public float startValue;
}

public class UpgradeSystem : MonoBehaviour
{
    private GameObject[] _upgradeButtonsTemp;
    private UpgradeButton[] _upgradeButtons = new UpgradeButton[8];
    private LevelManager _lvlManager;
    private List<Upgrades> _upgrades = new List<Upgrades>(8);
    private float jumpForce;
    private float rotationSpeed;
    private float fuelUsage;
    private float enginePower;
    private float acceleration;
    private float boostPower;
    private float moneyFrequency;
    private float wings;
    private Player _player;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        jumpForce = 1300;
        rotationSpeed = 3.8f;
        fuelUsage = .5f;
        enginePower = 50;
        acceleration = 305;
        boostPower = 500;
        _upgrades = InfoContainer.Instance.GetUpgrades();
        _upgradeButtonsTemp = GameObject.FindGameObjectsWithTag("UpgradeButton");
        for (int i = 0; i < _upgradeButtonsTemp.Length; i++)
            _upgradeButtons[i] = _upgradeButtonsTemp[i].GetComponent<UpgradeButton>();
    }

    private void UpdateData()
    {
        for (int i = 0; i < _upgradeButtonsTemp.Length; i++)
            _upgradeButtons[i].UpdateData();
    }
    
    public void ApplyUpgrades()
    {
        UpgradeProgression();
        _player.ApplyUpgrades(jumpForce, rotationSpeed, fuelUsage, enginePower, acceleration, wings);
        _lvlManager.SetBoostPower(boostPower);
        GameObject.Find("Spawner").GetComponent<Spawner>()._moneyFrequency = moneyFrequency;
    }
    
    public void GiveMeInfo(Upgrades.Name name, out int currentUpgrade,out int maxUpgrades, out int cost)
    {
        for (int i = 0; i < 8; i++)
        {
            if (name == _upgrades[i].upgradeName)
            {
                currentUpgrade = _upgrades[i].currentUpgrade;
                cost = GiveMeCost(name);
                maxUpgrades = _upgrades[i].maxUpgrades;
                return;
            }
        }
        currentUpgrade = 99;
        cost = 99;
        maxUpgrades = 999;
    }
    
    private int GiveMeCost(Upgrades.Name name)
    {
        int tempIndex = (int) name;
        // Progressions
        switch (name)
        {
            case Upgrades.Name.Boost:
                return _upgrades[tempIndex].startCost + _upgrades[tempIndex].currentUpgrade * 2 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Wing:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 3 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Fuel:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 4 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Speed:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 2 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Tail:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade  *7 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Money:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 2 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Jump:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 9 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Engine:
                return _upgrades[tempIndex].startCost +  _upgrades[tempIndex].currentUpgrade * 12 + _upgrades[tempIndex].startCost;
            default: return 0;
        }
    }
    
    public bool IsEnoughMoney(int cost)
    {
        return cost <= _lvlManager.GetMoney();
    }

    public void Upgrade(Upgrades.Name name)
    {
        
        for (int i = 0; i < 8; i++)
        {
            if (name == _upgrades[i].upgradeName)
            {
                _lvlManager.SpendMoney(GiveMeCost(name));
                _upgrades[i].currentUpgrade++;
            }
        }
        UpdateData();
    }

    private void UpgradeProgression()
    {
        boostPower = _upgrades[0].startValue + _upgrades[0].startValue * _upgrades[0].currentUpgrade;
        jumpForce = _upgrades[1].startValue + _upgrades[1].startValue * _upgrades[1].currentUpgrade;
        acceleration = _upgrades[2].startValue + _upgrades[2].startValue * _upgrades[2].currentUpgrade * .3f;
        moneyFrequency =_upgrades[3].startValue * 2 + _upgrades[3].startValue * _upgrades[3].currentUpgrade;
        fuelUsage = _upgrades[4].startValue + _upgrades[4].startValue * _upgrades[4].currentUpgrade;
        enginePower = _upgrades[5].startValue + _upgrades[5].startValue * _upgrades[5].currentUpgrade;
        wings = _upgrades[6].startValue + _upgrades[6].startValue * _upgrades[6].currentUpgrade * 15;
        rotationSpeed = _upgrades[7].startValue + _upgrades[7].startValue * _upgrades[7].currentUpgrade;
    }

    public void SaveLevelData(int currentLevel, int money)
    {
        InfoContainer.Instance.SaveLevelData(_upgrades, currentLevel, money);
    }
}
