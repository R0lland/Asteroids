using ServiceLocatorAsteroid.Service;
using System.Collections.Generic;
using UnityEngine;

public class BulletService : IBulletService
{
    private Bullet _bullet;
    private List<Bullet> _bulletsActive = new List<Bullet>();

    public BulletService(Bullet bullet)
    {
        _bullet = bullet;
    }

    public void PoolObjects(int amount)
    {
        ServiceLocator.Current.Get<IPoolingService>().AddObjectsToPool(_bullet, amount);
    }

    public void CreateBullet(HitType target, Vector3 position, Quaternion rotation)
    {
        GameObject bulletGameobject = ServiceLocator.Current.Get<IPoolingService>().GetFromPool(_bullet);
        Bullet bullet = bulletGameobject.GetComponent<Bullet>();
        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.Initialize(target, this);
        _bulletsActive.Add(bullet);
    }

    public void RemoveBullet(Bullet bullet)
    {
        _bulletsActive.Remove(bullet);
        bullet.OnDespawn();
    }

    public void DestroyAllBullets()
    {
        ServiceLocator.Current.Get<IPoolingService>().RemoveFromPool(_bullet);
    }
}
