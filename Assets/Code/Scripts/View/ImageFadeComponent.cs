using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ImageFadeComponent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _speed;

    private float _time;
    private float _minValue = 0f;
    private float _maxValue = 1f;

    private void Update()
    {
        _text.alpha = Mathf.Lerp(_minValue, _maxValue, _time);
        _time += Time.deltaTime * _speed;

        if (_time > 1.0f)
        {
            float temp = _maxValue;
            _maxValue = _minValue;
            _minValue = temp;
            _time = 0.0f;
        }
    }
}
