using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private int _boostPower;
    private int _moneyCost;
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
    
    
    private void Awake()
    {
        TemporaryInitializationDeleteMeBeforeBuildPls();
    }

    private void Start()
    {
        DrawMoney();
    }

    private void FixedUpdate()
    {
        _tempPosition = _player.position;
        distanceText.text = "Dist. " + ((int)_tempPosition.x).ToString();
        heightText.text = "Alt. " + ((int)_tempPosition.y).ToString();
        if (_tempPosition.x > distance)
            _finishGround.GetComponent<FinishGround>().StartShowing();
    }
    
    private void TemporaryInitializationDeleteMeBeforeBuildPls()
    {
        _boostPower = 550;
        _moneyCost = 1;
        _boostFrequency = 10;
        _moneyFrequency = 10;
        _money = 500;
        _currentLevel = 1; // TODO: change to info container
        _height = 2000;
        distance = 2000;
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void DrawMoney()
    {
        _moneyText.text = _money.ToString();
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

    public int GetMoney()
    {
        return _money;
    }

    public void SpendMoney(int howMuchMoney)
    {
        _money -= howMuchMoney;
        Debug.Log(_money);
    }
}
