using ServiceLocatorAsteroid.Service;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BulletService : IBulletService
{
    private Bullet _bullet;
    private List<Bullet> _bulletsActive = new List<Bullet>();

    private IPoolingService _pooledService;
    private IAssetsService _assetsService;

    public BulletService()
    {
        _pooledService = ServiceLocator.Current.Get<IPoolingService>();
        _assetsService = ServiceLocator.Current.Get<IAssetsService>();
    }

    public void PoolObjects(int amount)
    {
        _bullet = _assetsService.GetBullet();
        _pooledService.AddObjectsToPool(_bullet, amount);
    }

    public void CreateBullet(HitType target, Vector3 position, Quaternion rotation)
    {
        GameObject bulletGameobject = _pooledService.GetFromPool(_bullet);
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
        _pooledService.RemoveFromPool(_bullet);
    }
}
