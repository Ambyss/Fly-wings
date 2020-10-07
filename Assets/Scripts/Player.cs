using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool _acceleration;
    private Vector2 _planeRotation;
    private float _fuel;
    private float _maxFuel;
    [SerializeField] private GameObject _fuelLine;
    private bool _fuelUsage;
    private UpgradeSystem _upgradeSystem;
    private float _fuelUsageValue;
    private float _accelerationPower;
    private float _enginePower;
    private bool _fisnished;
    private float _upForce;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Acceleration());
        _jumpForce = new Vector2(0, 500); // TODO: delete this after adds to InfoContainer
        _verticalInput = 0;
        //_rotationSpeed = .8f; // TODO: Take this from container
        _rotationBoardUp = 45;
        _rotationBoardDown = 315;
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _rotationAcceleration = new Vector2(0, 40);
        _acceleration = true;
        _maxFuel = 100;
        _fuel = _maxFuel;
        FuelDraw();
        _fuelUsage = true;
        _upgradeSystem = GameObject.Find("UpgradeSystem").GetComponent<UpgradeSystem>();
        _fisnished = false;
    }
    
    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector2(0, _upForce));
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
        if (Input.GetKey(KeyCode.Space) && _fuelUsage)
            Engine();
    }

    private void Engine()
    {
        float temp = (transform.rotation.eulerAngles.z - 90) * Mathf.Deg2Rad;
        _planeRotation = new Vector2(0, Mathf.Cos(temp)).normalized;
        _rigidbody.AddForce(_planeRotation * _enginePower);
        _fuel -= 1 / _fuelUsageValue;
        if (_fuel <= 0)
            _fuelUsage = false;
        FuelDraw();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost"))
        {
            _rigidbody.AddForce(new Vector2(0, _levelManager.GetBoostPower()));
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Money"))
        {
            _levelManager.AddMoney();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Water"))
        {
            if (!_fisnished)
            {
                _fisnished = true;
                _levelManager.GameEnds(false);
                Destroy(this);
            }

        }
        else if (other.CompareTag("Finish"))
        {
            if (!_fisnished)
            {
                _levelManager.GameEnds(true);
                Destroy(this);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (!_fisnished)
            {
                _levelManager.GameEnds(true);
                Destroy(this);
            }
        }
    }

    private IEnumerator Acceleration()
    {
        _rigidbody.AddForce(new Vector2(_accelerationPower, 0));
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(Acceleration());
    }

    public void StopAcceleration()
    {
        _acceleration = false;
        StopAllCoroutines();
        _rigidbody.AddForce(_jumpForce);
    }

    private void FuelDraw()
    {
        _fuelLine.transform.localScale = new Vector3(_fuel/_maxFuel, 1, 1);
    }

    public void ApplyUpgrades(float jumpForce, float rotationSpeed, float fuelUsage, float enginePower, float acceleration, float wings) // Add wings
    {
        _jumpForce = new Vector2(0, jumpForce);
        _rotationSpeed = rotationSpeed;
        _fuelUsageValue = fuelUsage;
        _enginePower = enginePower;
        _accelerationPower = acceleration;
        _upForce = wings;
    }
}
