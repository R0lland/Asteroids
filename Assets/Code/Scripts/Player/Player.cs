using System;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    private Action _onHitTaken;

    public HitType hitType => HitType.Player;

    public void SetData(Action onHitTaken)
    {
        _onHitTaken = onHitTaken;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnHitTaken();
    }

    public void OnHitTaken()
    {
        _onHitTaken?.Invoke();
    }
}
