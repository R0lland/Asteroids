using ServiceLocatorAsteroid.Service;
using UnityEngine;

public interface IPoolingService : IGameService
{
    public void AddToPool(IPoolable i);
    public void RemoveFromPool(IPoolable i);
    public GameObject GetFromPool(IPoolable i);
    public void AddObjectsToPool(IPoolable poolable, int amount);
}
