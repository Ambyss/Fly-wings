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

}

public class UpgradeSystem : MonoBehaviour
{
    private GameObject[] _upgradeButtonsTemp;
    private UpgradeButton[] _upgradeButtons = new UpgradeButton[8];
    private LevelManager _lvlManager;
    private List<Upgrades> _upgrades = new List<Upgrades>(8);

    private void Start()
    {
        // TODO: take currentUpgrade from InfoContainer --> Saver
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Boost, currentUpgrade = 1, maxUpgrades = 5, startCost = 30});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Jump, currentUpgrade = 1, maxUpgrades = 5, startCost = 30});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Speed, currentUpgrade = 1, maxUpgrades = 5, startCost = 30});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Money, currentUpgrade = 1, maxUpgrades = 5, startCost = 30});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Fuel, currentUpgrade = 1, maxUpgrades = 5, startCost = 30});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Engine, currentUpgrade = 1, maxUpgrades = 5, startCost = 50});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Wing, currentUpgrade = 1, maxUpgrades = 5, startCost = 50});
        _upgrades.Add(new Upgrades() {upgradeName = Upgrades.Name.Tail, currentUpgrade = 1, maxUpgrades = 5, startCost = 50});

        _lvlManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _upgradeButtonsTemp = GameObject.FindGameObjectsWithTag("UpgradeButton");
        for (int i = 0; i < _upgradeButtonsTemp.Length; i++)
            _upgradeButtons[i] = _upgradeButtonsTemp[i].GetComponent<UpgradeButton>();
    }

    private void UpdateData()
    {
        for (int i = 0; i < _upgradeButtonsTemp.Length; i++)
            _upgradeButtons[i].UpdateData();
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
                return _upgrades[tempIndex].currentUpgrade * 2 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Wing:
                return _upgrades[tempIndex].currentUpgrade * 3 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Fuel:
                return _upgrades[tempIndex].currentUpgrade * 4 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Speed:
                return _upgrades[tempIndex].currentUpgrade * 2 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Tail:
                return _upgrades[tempIndex].currentUpgrade  *7 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Money:
                return _upgrades[tempIndex].currentUpgrade * 2 * _upgrades[tempIndex].startCost;
            case Upgrades.Name.Jump:
                return _upgrades[tempIndex].currentUpgrade * 9 + _upgrades[tempIndex].startCost;
            case Upgrades.Name.Engine:
                return _upgrades[tempIndex].currentUpgrade * 12 + _upgrades[tempIndex].startCost;
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
}
