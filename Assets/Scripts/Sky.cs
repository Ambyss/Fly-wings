using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    private Rigidbody2D _target;

    private void Start()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        StartCoroutine(Death());
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(_target.velocity.x * .015f, _target.velocity.y * .015f);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
