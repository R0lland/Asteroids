using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ConfigBullet config;
    [SerializeField] private SpriteRenderer _bulletSpriteRenderer;

    private HitType _target = HitType.None;
    private BulletManager _bulletManager;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * config.speed;
    }

    public void Initialize(HitType hitType, BulletManager bulletManager)
    {
        _target = hitType;
        _bulletManager = bulletManager;
        _bulletSpriteRenderer.color = hitType == HitType.Player ? Color.red : Color.white;
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
                _bulletManager.RemoveBullet(this);
            }
        }
        else if (_target == HitType.Player)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.OnHitTaken();
                _bulletManager.RemoveBullet(this);
            }
        }
    }
}
