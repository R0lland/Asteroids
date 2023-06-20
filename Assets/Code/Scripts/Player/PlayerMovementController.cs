using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ConfigPlayerMovement _playerMovementConfig;

    private Rigidbody2D _rBody;
    private float _thrust;
    private float _turnDirection;

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inputReader.Movement.enabled) 
        {
            _thrust = _inputReader.Movement.ReadValue<Vector2>().y;
            _turnDirection = _inputReader.Movement.ReadValue<Vector2>().x;
        }
            
    }

    private void FixedUpdate()
    {
        if (_thrust > 0)
        {
            _rBody.AddForce(transform.up * _playerMovementConfig.thrustSpeed * _thrust);
        }

        if (_turnDirection != 0f)
        {
            transform.Rotate(transform.forward, _turnDirection * -_playerMovementConfig.rotationSpeed);
        }
    }
}
