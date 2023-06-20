using ServiceLocatorAsteroid.Service;
using System;
using UnityEngine;
using static EnemyService;

public interface IEnemyService : IGameService
{
    public void Initialize(Action onAllEnemiesDestoyed, Action<int> onEnemyDestroyed);

    public void CreateEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation, int enemyStage = 0);

    public void RemoveEnemy(Enemy enemy);
    public void DestroyAllEnemies();
    public void CreateEnemyAsteroids(int amount);
    public void PoolObjects(int amount);
}
