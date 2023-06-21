using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private ConfigBullet config;
    [SerializeField] private SpriteRenderer _bulletSpriteRenderer;

    private HitType _target = HitType.None;
    private IBulletService _bulletService;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * config.speed;
    }

    public void Initialize(HitType hitType, IBulletService bulletService)
    {
        _target = hitType;
        _bulletService = bulletService;
        _bulletSpriteRenderer.color = hitType == HitType.Player ? Color.red : Color.white;
        Invoke(nameof(OnDespawn), config.lifeTime);
        OnSpawn();
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
                _bulletService.RemoveBullet(this);
            }
        }
        else if (_target == HitType.Player)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.OnHitTaken();
                _bulletService.RemoveBullet(this);
            }
        }
    }

    public void OnSpawn()
    {
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
}
