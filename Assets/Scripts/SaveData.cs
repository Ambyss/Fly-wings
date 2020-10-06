using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private Info _info = new Info();
    
    public void SaveIntoJson()
    {
        InitData();
        string info = JsonUtility.ToJson(_info);
        File.WriteAllText(Application.persistentDataPath + "/PotionData.json", info);
    }
    
    public void LoadFromJson()
    {
        Info info = JsonUtility.FromJson<Info>(File.ReadAllText(Application.persistentDataPath + "/PotionData.json"));
        InfoContainer.Instance.SetData(info.money, info.currentLevel, info.upgrades);
    }
    
    private void InitData()
    {
        _info.upgrades = InfoContainer.Instance.GetData(out _info.money, out _info.currentLevel);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Load");
            LoadFromJson();
        }
    }
}

[System.Serializable]
public class Info
{
    public int money;
    public int currentLevel;
    public List<UpgradeData> upgrades = new List<UpgradeData>(8);
}

[System.Serializable]
public class UpgradeData
{
    public Upgrades.Name upgradeName;
    public int currentUpgrade;
    public int maxUpgrades;
    public int startCost;
}
