using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoContainer : MonoBehaviour
{
    public static InfoContainer Instance;

    public int money;
    public int currentLevel;
    private List<Upgrades> _upgrades = new List<Upgrades>(8);
    private SaveData _saver;
    private LevelManager _levelManager;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        Initialization();
    }

    private void Initialization()
    {
        try
        {
            _saver = GameObject.Find("Saver").GetComponent<SaveData>();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
        }
        if (_saver != null)
        {
            if (_upgrades.Count == 0)
            {
                if (!File.Exists(Application.persistentDataPath + "/PotionData.json"))
                {
                    Debug.Log("fitst init");
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Boost, currentUpgrade = 0, maxUpgrades = 5, startCost = 30, startValue = 500});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Jump, currentUpgrade = 0, maxUpgrades = 5, startCost = 30, startValue = 500});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Speed, currentUpgrade = 0, maxUpgrades = 5, startCost = 30, startValue = 30});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Money, currentUpgrade = 0, maxUpgrades = 5, startCost = 30, startValue = 10});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Fuel, currentUpgrade = 0, maxUpgrades = 5, startCost = 30, startValue = .5f});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Engine, currentUpgrade = 0, maxUpgrades = 3, startCost = 50, startValue = 30});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Wing, currentUpgrade = 0, maxUpgrades = 3, startCost = 50, startValue = .05f});
                    _upgrades.Add(new Upgrades()
                        {upgradeName = Upgrades.Name.Tail, currentUpgrade = 0, maxUpgrades = 3, startCost = 50, startValue = .8f});
                    currentLevel = 1;
                    money = 1110;
                }
                else
                {
                    _saver.LoadFromJson();
                    Debug.Log("fie exist");
                }
            }
            else if (_upgrades.Count == 8)
            {
                _saver.SaveIntoJson();
            }
            else
            {
                Application.Quit();
                Debug.Log("FUCK");
            }
        }

        try
        {
            _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
        }

        if (_levelManager != null)
        {
            
        }
    }

    public List<Upgrades> GetUpgrades()
    {
        return _upgrades;
    }

    public List<UpgradeData> GetData(out int money, out int currentLevel)
    {
        money = this.money;
        currentLevel = this.currentLevel;
        List<UpgradeData> temp = new List<UpgradeData>(_upgrades.Capacity);
        for (int i = 0; i < 8; i++)
            temp.Add(new UpgradeData()
            {
                upgradeName = _upgrades[i].upgradeName, currentUpgrade = _upgrades[i].currentUpgrade,
                maxUpgrades = _upgrades[i].maxUpgrades, startCost = _upgrades[i].startCost, 
                startValue = _upgrades[i].startValue
            });
        return temp;
    }

    public void SetData(int money, int currentLevel, List<UpgradeData> upgrades)
    {
        this.money = money;
        this.currentLevel = currentLevel;
        for (int i = 0; i < 8; i++)
        {
            _upgrades.Add(new Upgrades()
            {
                upgradeName = upgrades[i].upgradeName, currentUpgrade = upgrades[i].currentUpgrade,
                maxUpgrades = upgrades[i].maxUpgrades, startCost = upgrades[i].startCost,
                startValue = upgrades[i].startValue
            });
        }
    }

    public void SaveLevelData(List<Upgrades> upgrades, int currentLevel, int money)
    {
        _upgrades = upgrades;
        this.currentLevel = currentLevel;
        this.money = money;
    }
}