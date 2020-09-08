using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoContainer : MonoBehaviour
{
    public static InfoContainer Instance;
    private int _boost;
    private int _money;
    private float _boostFrequency;
    private float _moneyFrequency;
    
    
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
        
        
    }

    
}
