using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
        public GameObject[] go;
        private Transform _target;
        public float _moneyFrequency;
        public GameObject[] _sky;
        public GameObject[] _asteroid;

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
                StartCoroutine(SpawnSky());
                StartCoroutine(SpawnAsteroids());
        }
        
        IEnumerator SpawnBoost()
        {
                transform.position = new Vector2(_target.position.x + 60, _target.position.y);
                Vector2 _randomPos = new Vector3(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2), 1);
                Instantiate(go[1], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(0.3f);
                StartCoroutine(SpawnBoost());
        }
        
        IEnumerator SpawnMoney()
        {
                Vector2 _randomPos = new Vector3(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2), 1);
                Instantiate(go[0], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(1/_moneyFrequency);
                StartCoroutine(SpawnMoney());
        }

        IEnumerator SpawnSky()
        {
                Vector2 _randomPos = new Vector3(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2), -1);
                if (transform.position.y > 1000 && transform.position.y < 8000)
                        Instantiate(_sky[Random.Range(0, _sky.Length)], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(1);
                StartCoroutine(SpawnSky());
                
        }

        IEnumerator SpawnAsteroids()
        {
                Vector2 _randomPos = new Vector3(transform.position.x + Random.Range(-transform.localScale.x/2, transform.localScale.x/2),
                        transform.position.y + Random.Range(-transform.localScale.y/2, transform.localScale.y/2), -1);
                if (transform.position.y > 10000)
                        Instantiate(_asteroid[Random.Range(0, _sky.Length)], _randomPos, quaternion.identity);
                yield return new WaitForSeconds(3);
                StartCoroutine(SpawnAsteroids());
        }
}
