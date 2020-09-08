using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _jumpForce; // TODO: Take this from InfoContainer
    private float _verticalInput;
    private float _rotationSpeed;
    private float _rotationBoardUp;
    private float _rotationBoardDown;
    private float _boostPower;
    private LevelManager _levelManager;
    private Vector2 _rotationAcceleration;
    public Canvas canvas;
    private bool _acceleration;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Acceleration());
        _jumpForce = new Vector2(0, 500); // TODO: delete this after adds to InfoContainer
        _verticalInput = 0;
        _rotationSpeed = .8f; // TODO: Take this from container
        _rotationBoardUp = 45;
        _rotationBoardDown = 315;
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _boostPower = _levelManager.GetBoostPower();
        _rotationAcceleration = new Vector2(0, 40);
        _acceleration = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) canvas.enabled = !canvas.isActiveAndEnabled;
        _verticalInput = Input.GetAxis("Horizontal");
        if (_acceleration) 
            _verticalInput = 0;
        if (_verticalInput != 0)
        {
            transform.RotateAround(transform.position, Vector3.forward, -_rotationSpeed * _verticalInput);
            _rigidbody.AddForce(-_rotationAcceleration * _rotationSpeed * _verticalInput);
            if (transform.rotation.eulerAngles.z > _rotationBoardUp &&
                transform.rotation.eulerAngles.z < _rotationBoardDown)
            {
                transform.RotateAround(transform.position, Vector3.forward, _verticalInput * _rotationSpeed);
                _rigidbody.AddForce(_rotationAcceleration * _rotationSpeed * _verticalInput);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost"))
        {
            _rigidbody.AddForce(new Vector2(0, _boostPower));
        }
        else if (other.CompareTag("Money"))
        {
            
        }
        else if (other.CompareTag("Water"))
        {
            _levelManager.GameEnds(false);
        }
        else if (other.CompareTag("Finish"))
        {
            _levelManager.GameEnds(true);
        }
    }

    private IEnumerator Acceleration()
    {
        _rigidbody.AddForce(new Vector2(35, 0));
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(Acceleration());
    }

    public void StopAcceleration()
    {
        _acceleration = false;
        StopAllCoroutines();
        _rigidbody.AddForce(_jumpForce);
    }
    
    
}
