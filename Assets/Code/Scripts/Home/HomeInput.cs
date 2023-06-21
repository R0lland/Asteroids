using System;
using UnityEngine;

public class HomeInput : MonoBehaviour
{
    private Action _onKeyPressed;

    public void Initialize(Action onKeyPressed)
    {
        _onKeyPressed = onKeyPressed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _onKeyPressed?.Invoke();
        }
    }
}
