using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class View : MonoBehaviour
{
    protected RectTransform _rectTransform;

    protected virtual void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas)
        {
            transform.SetParent(canvas.transform, false);
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = Vector3.zero;
            _rectTransform.localScale = Vector3.one;
        }
    }
}
