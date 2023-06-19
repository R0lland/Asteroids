using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : IGameService
{
    private Dictionary<string, IPooling> _poolingObjects = new Dictionary<string, IPooling>();

    public void AddToPool(IPooling i)
    {
        IPooling pooling = null;
        _poolingObjects.TryGetValue("aa", out pooling);
        pooling.OnSpawn();
    }

    public void GetObject(string type)
    {

    }
}
