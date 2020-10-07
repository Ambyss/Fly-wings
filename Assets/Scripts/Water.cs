﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _playerRB;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
        _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(_player.position.x, -55);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerRB.drag = 4;
            _playerRB.AddForce(new Vector2(0, 10));
        }
    }
}
