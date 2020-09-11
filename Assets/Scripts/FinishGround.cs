using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGround : MonoBehaviour
{
    private Transform _target;
    private int _targetDistance;

    private void Start()
    {
        _target = GameObject.Find("Player").GetComponent<Transform>();
        _targetDistance = GameObject.Find("LevelManager").GetComponent<LevelManager>().distance;
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(_target.position.x, -17);
    }

    public void StartShowing()
    {
        gameObject.SetActive(true);
    }
}
