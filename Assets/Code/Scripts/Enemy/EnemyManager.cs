using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IEnemyManager
{
    private Asteroid _asteroid;
    private Saucer _saucer;

    private List<Enemy> _enemiesActive = new List<Enemy>();
    private Action _onAllEnemiesDestroyed;
    private Action _onEnemyDestroyed;

    public EnemyManager(Asteroid asteroid, Saucer saucer)
    {
        _asteroid = asteroid;
        _saucer = saucer;
    }

    public void Initialize(Action onAllEnemiesDestoyed, Action<int> onEnemyDestroyed)
    {
        _onAllEnemiesDestroyed += onAllEnemiesDestoyed;
        onEnemyDestroyed += onEnemyDestroyed;
    }

    public void CreateEnemyAsteroid(Vector3 position, Quaternion rotation, int asteroidStage)
    {
        Asteroid asteroid = GameObject.Instantiate(_asteroid, position, rotation);
        asteroid.Initialize();
        asteroid.SetAsteroidStage(asteroidStage);
        _enemiesActive.Add(asteroid);
    }

    public void CreateEnemySaucer(Vector3 position, Quaternion rotation)
    {
        Saucer saucer = GameObject.Instantiate(_saucer, position, rotation);
        _saucer.Initialize();
        _enemiesActive.Add(_saucer);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesActive.Remove(enemy);
        GameObject.Destroy(enemy.gameObject);

        if (_enemiesActive.Count <= 0)
        {
            _onAllEnemiesDestroyed?.Invoke();
        }
    }
}
