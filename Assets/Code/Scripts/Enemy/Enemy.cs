using ServiceLocatorAsteroid.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable, IPoolable
{
    public HitType hitType => HitType.Enemy;

    protected IEnemyService _enemyManager;

    protected Action<int> _onEnemyDestroyed;

    protected float _speed;
    protected int _scoreValue;

    protected virtual void Update()
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    public virtual void OnHitTaken()
    {
        _onEnemyDestroyed?.Invoke(_scoreValue);
        Explode();
    }

    public virtual void Initialize(Action<int> onEnemyDestroyed)
    {
        if (_enemyManager == null)
        {
            _enemyManager = ServiceLocator.Current.Get<IEnemyService>();
        }
        _onEnemyDestroyed = onEnemyDestroyed;
        SetDirection();
    }

    protected virtual void Explode()
    {
        _enemyManager.RemoveEnemy(this);
    }

    protected void SetDirection()
    {
        transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360);
    }

    public void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
