using ServiceLocatorAsteroid.Service;
using UnityEngine;

public interface IBulletManager : IGameService
{
    public void CreateBullet(HitType target, Vector3 position, Quaternion rotation);

    public void RemoveBullet(Bullet bullet);
}
