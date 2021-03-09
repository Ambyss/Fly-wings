using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Vector2 _boost;
    private ParticleSystem _explosion;
    public GameObject[] _tnts;
    
    private void Start()
    {
        _boost = new Vector2(0, 500);
        _explosion = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().StopAcceleration();
            _explosion.Play();
            for (int i = 0; i < _tnts.Length; i++)
            {
                Destroy(_tnts[i]);
            }
        }
    }
}
