using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    public void OnSpawn();

    public void OnDespawn();

    GameObject gameObject { get; }
}
