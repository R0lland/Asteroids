using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : IGameService
{
    private Asteroid _asteroid;

    private List<Enemy> _enemiesActive = new List<Enemy>();

    public EnemyManager(Asteroid asteroid)
    {
        _asteroid = asteroid;
    }

    public void CreateEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = GameObject.Instantiate(_asteroid, position, rotation);
        _asteroid.Initialize(this);
        _enemiesActive.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesActive.Remove(enemy);
        GameObject.Destroy(enemy.gameObject);
    }
}
