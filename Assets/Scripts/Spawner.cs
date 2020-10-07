using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
        public GameObject[] go;
        private Vector2 _randomPos;
        private Transform _target;
        public float _moneyFrequency;
        
        private void Start()
        {
                _target = UnityEngine.Camera.main.GetComponent<Transform>();
                StartCoroutine(StartSpawn());
        }

        IEnumerator StartSpawn()
        {
                yield return new WaitForSeconds(3);
                StartCoroutine(SpawnBoost());
                StartCoroutine(SpawnMoney());
        }
        
        IEnumerator SpawnBoost()
        {
                transform.position = new Vector2(_target.position.x + 60, _target.position.y);
                _randomPos = new Vector2(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2));
                Instantiate(go[1], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(SpawnBoost());
        }
        
        IEnumerator SpawnMoney()
        {
                transform.position = new Vector2(_target.position.x + 60, _target.position.y);
                _randomPos = new Vector2(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2));
                Instantiate(go[0], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(1/_moneyFrequency);
                StartCoroutine(SpawnMoney());
        }
}
