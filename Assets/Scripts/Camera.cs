using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 _target;
    private Transform _targetTransform;
    private Rigidbody2D _targetRigidbody;
    private Vector2 _targetVelocity;
    
    private void Start()
    {
        _targetTransform = GameObject.Find("Player").GetComponent<Transform>();
        _targetRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        _target = _targetTransform.position;
        _targetVelocity = _targetRigidbody.velocity;
        transform.position = new Vector3(_target.x + _targetVelocity.x * .15f, _target.y + _targetVelocity.y * .05f, -10);
    }
}
