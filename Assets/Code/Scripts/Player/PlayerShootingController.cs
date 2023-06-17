using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private Transform _bulletPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private InputReader _inputReader;

    private void Start()
    {
        _inputReader.SetActionInput(Fire);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Instantiate(_bullet, _bulletPoint.position, _bulletPoint.rotation);
    }
}
