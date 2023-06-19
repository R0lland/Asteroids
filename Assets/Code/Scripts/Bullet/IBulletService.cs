using ServiceLocatorAsteroid.Service;
using UnityEngine;

public interface IBulletService : IGameService
{
    public void CreateBullet(HitType target, Vector3 position, Quaternion rotation);

    public void RemoveBullet(Bullet bullet);
}
