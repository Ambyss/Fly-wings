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

    private void Start()
    {
        _groundReached.enabled = false;
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

    public void GroundReached()
    {
        //StartCoroutine(TextFlashing());
        _groundReached.enabled = true;
    }

    private IEnumerator TextFlashing()
    {
        _groundReached.enabled = true;
        yield return new WaitForSeconds(.8f);
        _groundReached.enabled = false;
        yield return new WaitForSeconds(.8f);
        StartCoroutine(TextFlashing());
    }
}
