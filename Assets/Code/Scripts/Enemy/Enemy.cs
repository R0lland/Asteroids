using ServiceLocatorAsteroid.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    public HitType hitType => HitType.Enemy;

    protected float _speed;
    protected IEnemyManager _enemyManager;

    protected virtual void Update()
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    public virtual void OnHitTaken()
    {
        
    }

    public virtual void Initialize()
    {
        if (_enemyManager == null)
        {
            _enemyManager = ServiceLocator.Current.Get<IEnemyManager>();
        }
        SetDirection();
    }

    protected void SetDirection()
    {
        transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360);
    }
}
