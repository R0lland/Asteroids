using System;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    private Action _onHitTaken;

    public HitType hitType => HitType.Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitTaken();
        }
    }

    public void SetData(Action onHitTaken)
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

    }
}
