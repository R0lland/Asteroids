using System.Collections.Generic;
using UnityEngine;

public class PoolingObjectData
{
    private List<GameObject> _poolingObjectsList;
    private Transform _parent;
    private int _lastIndexSelected;

    public List<GameObject> PoolingObjectsList
    {
        get { return _poolingObjectsList; }
    }
    public int LastIndexSelected
    {
        get { return _lastIndexSelected; }
    }

    public Transform Parent
    {
        get { return _parent; }
    }

    public PoolingObjectData(int lastIndexSelected, Transform parent, List<GameObject> poolable)
    {
        _lastIndexSelected = lastIndexSelected;
        _parent = parent;
        _poolingObjectsList = poolable;
    }

    public void SetLastIndex(int lastIndexSelected)
    {
        _lastIndexSelected = lastIndexSelected;
    }
}
