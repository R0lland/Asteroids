using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ConfigPlayerMovement _playerMovementConfig;

    private Rigidbody2D _rBody;
    private float _thrust;
    private float turnDirection;

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _thrust = _inputReader.Movement.ReadValue<Vector2>().y;
        turnDirection = _inputReader.Movement.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        if (_thrust > 0)
        {
            _rBody.AddForce(transform.up * _playerMovementConfig.thrustSpeed * _thrust);
        }

        if (turnDirection != 0f)
        {
            _rBody.AddTorque(turnDirection * -_playerMovementConfig.rotationSpeed);
        }
    }
}
