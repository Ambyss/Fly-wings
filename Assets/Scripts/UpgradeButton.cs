using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private UpgradeSystem _US;
    private int _maxUpgrages;
    private int _currentUpgrade;
    private bool _isEnoughMoney;
    
    
    private void Start()
    {
        _US = transform.parent.gameObject.GetComponent<UpgradeSystem>();
    }

    
}
