using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : IEnemyService
{
    public enum EnemyType
    {
        None,
        Asteroid,
        Saucer
    }

    private Asteroid _asteroid;
    private Saucer _saucer;

    private IPoolingService _poolingService;

    private List<Enemy> _enemiesActive = new List<Enemy>();
    private Action _onAllEnemiesDestroyed;
    private Action<int> _onEnemyDestroyed;

    private Dictionary<EnemyType, Enemy> enemyTypes = new Dictionary<EnemyType, Enemy>();

    public EnemyService(Asteroid asteroid, Saucer saucer)
    {
        _asteroid = asteroid;
        _saucer = saucer;

        enemyTypes.Add(EnemyType.Asteroid, _asteroid);
        enemyTypes.Add(EnemyType.Saucer, _saucer);

        _poolingService = ServiceLocator.Current.Get<IPoolingService>();
    }

    public void PoolObjects(int amount)
    {
        _poolingService.AddObjectsToPool(_asteroid, amount);
    }

    public void Initialize(Action onAllEnemiesDestoyed, Action<int> onEnemyDestroyed)
    {
        _onAllEnemiesDestroyed = onAllEnemiesDestoyed;
        _onEnemyDestroyed = onEnemyDestroyed;
    }

    public void CreateEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation, int enemyStage = 0)
    {
        switch (enemyType)
        {
            case EnemyType.None:
                break;
            case EnemyType.Asteroid:
                CreateEnemyAsteroid(position, rotation, enemyStage);
                break;
            case EnemyType.Saucer:
                CreateEnemySaucer(position, rotation);
                break;
        }
    }

    public void CreateEnemyAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * 5f;
            CreateEnemyAsteroid(spawnDirection, new Quaternion(0f, 0f, 0f, 0f), 0);
        }
    }

    private void CreateEnemyAsteroid(Vector3 position, Quaternion rotation, int asteroidStage)
    {
        GameObject asteroidGameobject = _poolingService.GetFromPool(_asteroid);
        Asteroid asteroid = asteroidGameobject.GetComponent<Asteroid>();
        asteroid.transform.SetPositionAndRotation(position, rotation);
        asteroid.Initialize(_onEnemyDestroyed);
        asteroid.SetAsteroidStage(asteroidStage);
        _enemiesActive.Add(asteroid);
    }

    private void CreateEnemySaucer(Vector3 position, Quaternion rotation)
    {
        Saucer saucer = GameObject.Instantiate(_saucer, position, rotation);
        _saucer.Initialize(_onEnemyDestroyed);
        _enemiesActive.Add(_saucer);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesActive.Remove(enemy);
        enemy.OnDespawn();

        if (_enemiesActive.Count <= 0)
        {
            _onAllEnemiesDestroyed?.Invoke();
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in _enemiesActive)
        {
            enemy.OnDespawn();
        }
        _enemiesActive.Clear();
    }
}
