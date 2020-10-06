using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Vector2 _boost;
    private ParticleSystem _explosion;
    
    private void Start()
    {
        _boost = new Vector2(0, 500);
        _explosion = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<Player>().StopAcceleration();
            _explosion.Play();
        }
    }
}
