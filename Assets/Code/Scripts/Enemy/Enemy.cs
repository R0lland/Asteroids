using ServiceLocatorAsteroid.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    public HitType hitType => HitType.Enemy;


    protected EnemyManager _enemyManager;

    public virtual void OnHitTaken()
    {
        
    }

    public virtual void Initialize()
    {
        if (_enemyManager == null)
        {
            _enemyManager = ServiceLocator.Current.Get<EnemyManager>();
        }
    }
}
