using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputAction _movement;
    [SerializeField] private InputAction _action;

    public InputAction Movement { get { return _movement; } set { } }

    private void OnEnable()
    {
        _movement.Enable();
        _action.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
        _action.Disable();
    }

    public void SetActionInput(Action<InputAction.CallbackContext> actionInput)
    {
        _action.performed += actionInput;
    }
}
