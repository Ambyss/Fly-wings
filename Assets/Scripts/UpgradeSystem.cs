using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{ 
    public enum Name
    {
        Jump,
        Money,
        Speed,
        Boost,
        Tail,
        Wing,
        Fuel,
        Engine
    };

    public Name name;
    public int currentUpgrade;
    public int maxUpgrades;
}


public class UpgradeSystem : MonoBehaviour
{
    private int _money;

   
    private List<Upgrades> _upgrades = new List<Upgrades>();

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            _upgrades.Add(new Upgrades());
            _upgrades[i].name = (Upgrades.Name) i;
            _upgrades[i].currentUpgrade = 0;
            _upgrades[i].maxUpgrades = 5;
        }
    }

    public void isEnoughMoney(string name)
    {
        
    }
}
