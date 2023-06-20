using ServiceLocatorAsteroid.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingService : IPoolingService
{
    public struct PoolingObjectData
    {
        public List<GameObject> poolingObjectsList;
        public int lastIndexSelected;
        public Transform parent;

        public PoolingObjectData(int lastIndexSelected, Transform parent, List<GameObject> poolable)
        {
            this.lastIndexSelected = lastIndexSelected;
            this.parent = parent;
            this.poolingObjectsList = poolable;
        }

        public void SetLastIndex(int lastIndexSelected)
        {
            this.lastIndexSelected = lastIndexSelected;
        }
    }

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
            poolingObjectData.poolingObjectsList.Add(newPoolable);
            newPoolable.transform.SetParent(parentGameobject);

            _poolingObjects.Add(name, poolingObjectData);
        }
        else
        {
            _poolingObjects[name].poolingObjectsList.Add(newPoolable);
            newPoolable.transform.SetParent(_poolingObjects[name].parent);
        }
    }

    public void RemoveFromPool(IPoolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_poolingObjects.ContainsKey(name))
        {
            GameObject.Destroy(_poolingObjects[name].parent.gameObject);
            _poolingObjects[name].poolingObjectsList.Clear();
            _poolingObjects.Remove(name);
        }
    }

    public GameObject GetFromPool(IPoolable poolable)
    {
       string name = poolable.gameObject.name;

        if (_poolingObjects.ContainsKey(name))
        {
            int lastIndex = _poolingObjects[name].lastIndexSelected;
            GameObject poolableObject = _poolingObjects[name].poolingObjectsList[lastIndex];

            int newIndex = lastIndex + 1;

            PoolingObjectData poolingObjectData = _poolingObjects[name];
            poolingObjectData.lastIndexSelected = newIndex;
            

            if (newIndex >= _poolingObjects[name].poolingObjectsList.Count)
            {
                poolingObjectData.lastIndexSelected = 0;
            }
            else
            {
                poolingObjectData.lastIndexSelected = newIndex;
            }
            _poolingObjects[name] = poolingObjectData;
            poolableObject.SetActive(true);
            return poolableObject;
        }
        return null;
    }
}
