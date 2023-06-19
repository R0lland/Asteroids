using ServiceLocatorAsteroid.Service;
using System;
using UnityEngine;

public interface IEnemyManager : IGameService
{
    public void Initialize(Action onAllEnemiesDestoyed);

    public void CreateEnemyAsteroid(Vector3 position, Quaternion rotation, int asteroidStage);

    public void CreateEnemySaucer(Vector3 position, Quaternion rotation);

    public void RemoveEnemy(Enemy enemy);
}
