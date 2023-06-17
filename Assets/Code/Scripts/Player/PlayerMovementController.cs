using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private const float PLAYER_FORWARD_SPEED = 2f;
    private const float PLAYER_TURN_SPEED = 5f;

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
            _rBody.AddForce(transform.up * PLAYER_FORWARD_SPEED * _thrust);
        }

        if (turnDirection != 0f)
        {
            _rBody.AddTorque(turnDirection * -PLAYER_TURN_SPEED);
        }
    }
}
