using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : Enemy
{
    [SerializeField] private List<ConfigSaucer> _configSaucer;

    private Coroutine _shootingRoutine;
    private IBulletManager _bulletManager;
    private Transform _playerTransform;

    private ConfigSaucer _config;

    private void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        _bulletManager = ServiceLocator.Current.Get<IBulletManager>();
        
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
    }

    private void SetBehaviour()
    {
        _speed = Random.Range(_config.minSpeed, _config.maxSpeed);
        transform.localScale = new Vector3(_config.size, _config.size, _config.size);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void Fire()
    {
        _bulletManager.CreateBullet(HitType.Player, transform.position, transform.rotation);
    }

    private void Explode()
    {
        _enemyManager.RemoveEnemy(this);
    }

    public override void OnHitTaken()
    {
        ServiceLocator.Current.Get<IGameManager>().Score(0);
        Explode();
    }
}
