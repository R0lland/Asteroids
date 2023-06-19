using ServiceLocatorAsteroid.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private InputReader _inputReader;

    private IBulletService _bulletManager;

    private void Start()
    {
        _inputReader.SetActionInput(Fire);
        _bulletManager = ServiceLocator.Current.Get<IBulletService>();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        _bulletManager.CreateBullet(HitType.Enemy, _bulletPoint.position, _bulletPoint.rotation);
    }
}
