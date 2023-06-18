using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ConfigBullet config;

    private HitType _target = HitType.None;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * config.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target == HitType.None) { return; }

        if (_target == HitType.Enemy)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnHitTaken();
            }
        }
        else if (_target == HitType.Player)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.OnHitTaken();
            }
        }
    }

    public void SetTarget(HitType hitType)
    {
        _target = hitType;
    }
}
