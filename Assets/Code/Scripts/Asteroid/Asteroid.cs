using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }
}
