using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyService : IEnemyService
{
    private Asteroid _asteroid;
    private Saucer _saucer;

    private List<Enemy> _enemiesActive = new List<Enemy>();
    private Action _onAllEnemiesDestroyed;
    private Action<int> _onEnemyDestroyed;

    public EnemyService(Asteroid asteroid, Saucer saucer)
    {
        _asteroid = asteroid;
        _saucer = saucer;
    }

    public void PoolObjects(int amount)
    {
        ServiceLocator.Current.Get<IPoolingService>().AddObjectsToPool(_asteroid, amount);
    }

    public void Initialize(Action onAllEnemiesDestoyed, Action<int> onEnemyDestroyed)
    {
        _onAllEnemiesDestroyed = onAllEnemiesDestoyed;
        _onEnemyDestroyed = onEnemyDestroyed;
    }

    public void CreateEnemyAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * 5f;
            CreateEnemyAsteroid(spawnDirection, new Quaternion(0f, 0f, 0f, 0f), 0);
        }
    }

    public void CreateEnemyAsteroid(Vector3 position, Quaternion rotation, int asteroidStage)
    {
        GameObject asteroidGameobject = ServiceLocator.Current.Get<IPoolingService>().GetFromPool(_asteroid);
        Asteroid asteroid = asteroidGameobject.GetComponent<Asteroid>(); //GameObject.Instantiate(_asteroid, position, rotation);
        asteroid.transform.SetPositionAndRotation(position, rotation);
        asteroid.Initialize(_onEnemyDestroyed);
        asteroid.SetAsteroidStage(asteroidStage);
        _enemiesActive.Add(asteroid);
    }

    public void CreateEnemySaucer(Vector3 position, Quaternion rotation)
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
