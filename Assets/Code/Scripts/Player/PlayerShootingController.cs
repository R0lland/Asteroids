using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingController : MonoBehaviour
{
    [SerializeField] private InputAction _shootingInput;
    [SerializeField] private Transform _bulletPoint;

    private void OnEnable()
    {
        _shootingInput.Enable();
        _shootingInput.performed += Fire;
    }

    private void OnDisable()
    {
        _shootingInput.Disable();
        _shootingInput.performed -= Fire;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Shooting");
    }
}
