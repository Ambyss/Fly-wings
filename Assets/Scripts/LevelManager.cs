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
    private Vector3 _tempPosition;
    [SerializeField] private GameObject[] _finishGround;
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
    private bool _isFinish;
    private Text _groundReacherd;

    private void Start()         
    {
        _infoContainer = InfoContainer.Instance;
        _money = _infoContainer.money;
        _currentLevel = _infoContainer.currentLevel;
        _upgrades = _infoContainer.GetUpgrades();
        _moneyForLevel = 0;
        _lvlEndsCanvas.enabled = false;
        _upgradeCanvas.enabled = true;
        _player = GameObject.Find("Player").GetComponent<Transform>();
        DrawMoney();
        DistAndAltForLevel();
        Time.timeScale = 0;
        _maxHeight = 0;
        _upgradeSystem = GameObject.Find("UpgradeSystem").GetComponent<UpgradeSystem>();
        _isFinish = false;
    }

    private void FixedUpdate()
    {
        _tempPosition = _player.position;
        distanceText.text = "Dist. " + ((int)_tempPosition.x).ToString();
        heightText.text = "Alt. " + ((int)_tempPosition.y).ToString();
        if (_tempPosition.y > _maxHeight)
            _maxHeight = (int)_tempPosition.y;
        if (_tempPosition.x > distance)
        {
            for (int i = 0; i < _finishGround.Length; i++)
            {
                _finishGround[i].GetComponent<FinishGround>().StartShowing(i * 160);
            }
        }
    }

    private void DrawMoney()
    {
        _moneyText.text = _money.ToString();
    }
    
    public float GetBoostPower()
    {
        return boostPower;
    }

    public void SetBoostPower(float boostPower)
    {
        this.boostPower = boostPower;
    }
    
    public void GameEnds(bool isWin)
    {
        if (isWin && !_isFinish)
        {
            _isFinish = true;
            if (_maxHeight > _height && (int) _tempPosition.x > distance)
            {
                _moneyForLevel += _currentLevel * 100;
                _currentLevel++;
            }
        }

        _moneyForLevel += (int)(_tempPosition.x * .008f);
        _moneyForLevel += (int)(_maxHeight * .023f);
        _money += _moneyForLevel;
        _infoContainer.money = _money;
        DrawMoney();
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2);
        _lvlEndsCanvas.enabled = true;
        _lvlEndsAltitude.text = "Altitude: " + _maxHeight + " / " + _height + " (" + ((int)(_maxHeight * .018f) + " coins)");
        _lvlEndsDistance.text = "Distance: " + ((int)_tempPosition.x) + " / " + distance + " (" + ((int)(_tempPosition.x * .08f) + " coins)");
        _lvlEndsMoney.text = "Money: " + _moneyForLevel;
    }

    private void DistAndAltForLevel()
    {
        distance = (int)(2000 * _currentLevel * _currentLevel * .5f + 1000);
        _height = (int)(1000 * _currentLevel * _currentLevel * .1f + 500);
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

    public void AddMoney()
    {
        _moneyForLevel++;
    }

    public void LoadMenu()
    {
        _upgradeSystem.SaveLevelData(_currentLevel, _money);
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
