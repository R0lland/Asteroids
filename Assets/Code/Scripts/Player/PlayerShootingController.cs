using ServiceLocatorAsteroid.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private InputReader _inputReader;

    private IBulletService _bulletService;

    private void Start()
    {
        _inputReader.SetActionInput(Fire);
        _bulletService = ServiceLocator.Current.Get<IBulletService>();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        _bulletService.CreateBullet(HitType.Enemy, _bulletPoint.position, _bulletPoint.rotation);
    }
}
