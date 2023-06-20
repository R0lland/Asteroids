using System;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    public HitType hitType => HitType.Player;

    private Action _onHitTaken;

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
        Explode();
        _onHitTaken?.Invoke();
    }

    private void Explode()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        gameObject.SetActive(true);
    }
}
