using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    private Action _onKeyPressed;

    public void Initialize(Action onKeyPressed)
    {
        _onKeyPressed = onKeyPressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _onKeyPressed?.Invoke();
        }
    }
}
