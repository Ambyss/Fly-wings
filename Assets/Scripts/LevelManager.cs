using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private InfoContainer _infoContainer;
    public float boostPower;
    private float _boostFrequency;
    private float _moneyFrequency;
    private int _money;
    [SerializeField ]private Text _moneyText;
    private int _currentLevel;
    private int _height;
    public int distance;
    private Transform _player;
    public Text heightText;
    public Text distanceText;
    private int _tempDistance;
    private Vector3 _tempPosition;
    [SerializeField] private GameObject _finishGround;
    private List<Upgrades> _upgrades = new List<Upgrades>(8);
    public float _maxFuel;
    private int _moneyForLevel;
    [SerializeField] private Canvas _lvlEndsCanvas;
    [SerializeField] private Canvas _upgradeCanvas;
    [SerializeField] private Text _lvlEndsMoney;
    [SerializeField] private Text _lvlEndsDistance;
    [SerializeField] private Text _lvlEndsAltitude;
    private int _maxHeight;
    private UpgradeSystem _upgradeSystem;
    
    private void Awake()
    {
        TemporaryInitializationDeleteMeBeforeBuildPls();
    }

    private void Start()
    {
        _infoContainer = InfoContainer.Instance;
        _upgrades = _infoContainer.GetUpgrades();
        _moneyForLevel = 0;
        _lvlEndsCanvas.enabled = false;
        _upgradeCanvas.enabled = true;
        DrawMoney();
        Time.timeScale = 0;
        _maxHeight = 0;
        _upgradeSystem = GameObject.Find("UpgradeSystem").GetComponent<UpgradeSystem>();
    }

    private void FixedUpdate()
    {
        _tempPosition = _player.position;
        distanceText.text = "Dist. " + ((int)_tempPosition.x).ToString();
        heightText.text = "Alt. " + ((int)_tempPosition.y).ToString();
        if (_tempPosition.y > _maxHeight)
            _maxHeight = (int)_tempPosition.y;
        if (_tempPosition.x > distance)
            _finishGround.GetComponent<FinishGround>().StartShowing();
    }
    
    private void TemporaryInitializationDeleteMeBeforeBuildPls()
    {
        // TODO: change to info container
        _money = 500;
        
        _boostFrequency = 10;
        _moneyFrequency = 10;
        
        _currentLevel = 1; 
        _height = 2000;
        distance = 2000;
        _maxFuel = 500;
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void DrawMoney()
    {
        _moneyText.text = _money.ToString();
    }
    
    public float GetBoostPower()
    {
        Debug.Log(boostPower);
        return boostPower;
    }

    public void SetBoostPower(float boostPower)
    {
        this.boostPower = boostPower;
    }
    
    public void GameEnds(bool isWin)
    {
        if (isWin)
        {
            _currentLevel++;
        }
        Debug.Log(_money);
        Debug.Log(_moneyForLevel);
        _money += _moneyForLevel;
        DrawMoney();
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        _lvlEndsCanvas.enabled = true;
        _lvlEndsAltitude.text = "Altitude: " + _maxHeight.ToString();
        _lvlEndsDistance.text = "Distance: " + ((int)_tempPosition.x).ToString();
        _lvlEndsMoney.text = "Money: " + _moneyForLevel.ToString();
    }
    
    public int GetMoney()
    {
        return _money;
    }

    public void SpendMoney(int howMuchMoney)
    {
        _money -= howMuchMoney;
        DrawMoney();
    }

    public List<Upgrades> GetUpgrades()
    {
        return _upgrades;
    }

    public void AddMoney()
    {
        _moneyForLevel++;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void PlayGame()
    {
        _upgradeCanvas.enabled = false;
        Time.timeScale = 1;
        _upgradeSystem.ApplyUpgrades();
    }
}
