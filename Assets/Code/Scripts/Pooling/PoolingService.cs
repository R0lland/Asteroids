using System.Collections.Generic;
using UnityEngine;

public class PoolingService : IPoolingService
{
    private Dictionary<string, PoolingObjectData> _poolingObjects = new Dictionary<string, PoolingObjectData>();

    public void AddObjectsToPool(IPoolable poolable, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddToPool(poolable);
        }
    }

    public void AddToPool(IPoolable poolable)
    {
        GameObject newPoolable = GameObject.Instantiate(poolable.gameObject);
        
        string name = poolable.gameObject.name;
        newPoolable.SetActive(false);

        if (!_poolingObjects.ContainsKey(name))
        {
            Transform parentGameobject = new GameObject(name).transform;
            PoolingObjectData poolingObjectData = new PoolingObjectData(0, parentGameobject, new List<GameObject>());
            poolingObjectData.PoolingObjectsList.Add(newPoolable);
            newPoolable.transform.SetParent(parentGameobject);

            _poolingObjects.Add(name, poolingObjectData);
        }
        else
        {
            _poolingObjects[name].PoolingObjectsList.Add(newPoolable);
            newPoolable.transform.SetParent(_poolingObjects[name].Parent);
        }
    }

    public void RemoveFromPool(IPoolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_poolingObjects.ContainsKey(name))
        {
            GameObject.Destroy(_poolingObjects[name].Parent.gameObject);
            _poolingObjects[name].PoolingObjectsList.Clear();
            _poolingObjects.Remove(name);
        }
    }

    public GameObject GetFromPool(IPoolable poolable)
    {
       string name = poolable.gameObject.name;

        if (_poolingObjects.ContainsKey(name))
        {
            int lastIndex = _poolingObjects[name].LastIndexSelected;
            GameObject poolableObject = _poolingObjects[name].PoolingObjectsList[lastIndex];

            int newIndex = _poolingObjects[name].LastIndexSelected + 1;

            if (newIndex >= _poolingObjects[name].PoolingObjectsList.Count)
            {
                newIndex = 0;
            }
            _poolingObjects[name].SetLastIndex(newIndex);

            poolableObject.SetActive(true);
            return poolableObject;
        }
        return null;
    }

    
}
