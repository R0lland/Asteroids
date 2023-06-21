using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    public HitType hitType => HitType.Player;

    private Action _onHitTaken;
    private Coroutine _waitInvencibility;
    private bool _isInvincible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitTaken();
        }
    }

    public void Initialize(Action onHitTaken)
    {
        _onHitTaken = onHitTaken;
    }

    public void OnHitTaken()
    {
        if (_isInvincible) return;
        Activate(false);
        _onHitTaken?.Invoke();
    }

    public void Activate(bool activate)
    {
        gameObject.SetActive(activate);
    }

    public void Respawn()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        gameObject.SetActive(true);
        SetInvincible();
    }

    private void SetInvincible()
    {
        _isInvincible = true;
        if (_waitInvencibility != null) StopCoroutine(_waitInvencibility);
        _waitInvencibility = StartCoroutine(WaitInvencibility(2f));
    }
    
    public void ShutDown()
    {
        _onHitTaken = null;
    }

    private IEnumerator WaitInvencibility(float duration)
    {
        yield return new WaitForSeconds(duration);
        _isInvincible = false;
    }
}
