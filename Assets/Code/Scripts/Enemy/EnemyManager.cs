using ServiceLocatorAsteroid.Service;
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

    public void CreateEnemyAsteroid(Vector3 position, Quaternion rotation, int asteroidStage)
    {
        Asteroid asteroid = GameObject.Instantiate(_asteroid, position, rotation);
        asteroid.Initialize();
        asteroid.SetAsteroidStage(asteroidStage);
        _enemiesActive.Add(asteroid);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemiesActive.Remove(enemy);
        GameObject.Destroy(enemy.gameObject);
    }
}
