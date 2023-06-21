using ServiceLocatorAsteroid.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : Enemy
{
    [SerializeField] private List<ConfigSaucer> _configSaucer;

    private IBulletService _bulletService;

    private Coroutine _shootingRoutine;
    private Transform _playerTransform;
    private ConfigSaucer _config;

    public override void Initialize(Action<int> onEnemyDestroyed)
    {
        base.Initialize(onEnemyDestroyed);
        _bulletService = ServiceLocator.Current.Get<IBulletService>();
        
        Player p = FindObjectOfType<Player>();
        if (p)
        {
            _playerTransform = FindObjectOfType<Transform>();
        }
        SetSaucerType(0);
        SetBehaviour();
        InvokeRepeating("Fire", 1.0f, _config.fireRate);
    }

    private void SetSaucerType(int id)
    {
        for (int i = 0; i < _configSaucer.Count; i++)
        {
            if (_configSaucer[i].id == id)
            {
                _config = _configSaucer[i];
            }
        }
        _scoreValue = _config.scoreValue;
    }

    private void SetBehaviour()
    {
        _speed = UnityEngine.Random.Range(_config.minSpeed, _config.maxSpeed);
        transform.localScale = new Vector3(_config.size, _config.size, _config.size);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Fire()
    {
        _bulletService.CreateBullet(HitType.Player, transform.position, transform.rotation);
    }
}
