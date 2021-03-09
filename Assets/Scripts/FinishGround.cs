using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGround : MonoBehaviour
{
    private Transform _target;
    private int _targetDistance;
    [SerializeField] private Text _groundReached;
    private bool isShowed;

    private void Start()
    {
        isShowed = false;
        _groundReached.enabled = false;
        _target = GameObject.Find("Player").GetComponent<Transform>();
        _targetDistance = GameObject.Find("LevelManager").GetComponent<LevelManager>().distance;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_target.position.x > transform.position.x + 150)
        {
            transform.position = new Vector3(transform.position.x + 160, transform.position.y, transform.position.z);
        }
        //transform.position = new Vector2(_target.position.x, -17);
    }

    public void StartShowing(int dist)
    {
        if (!isShowed)
        {
            gameObject.SetActive(true);
            transform.position = new Vector3(_target.position.x + dist, transform.position.y, transform.position.z);
            GroundReached();
            isShowed = true;
        }
    }

    public void GroundReached()
    {
        _groundReached.enabled = true;
    }
}
