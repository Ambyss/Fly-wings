using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(_player.position.x, -17);
    }
}
