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

    public void CreateBullet(HitType target, Vector3 position, Quaternion rotation)
    {
        Bullet bullet = GameObject.Instantiate(_bullet, position, rotation);
        bullet.Initialize(target, this);
        _bulletsActive.Add(bullet);
    }

    public void RemoveBullet(Bullet bullet)
    {
        _bulletsActive.Remove(bullet);
        GameObject.Destroy(bullet.gameObject);
    }
}
