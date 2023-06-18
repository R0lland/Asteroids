using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    [SerializeField] ConfigAsteroid _configAsteroid;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private float _speed = 0.3f;
    private int _currentStage = 3;

    private void Update()
    {
        transform.position += transform.up * Time.deltaTime * _speed;
    }

    public override void Initialize(EnemyManager enemyManager)
    {
        base.Initialize(enemyManager);
        SetRandomSprite();
    }

    private void SetDirection()
    {

    }

    private void SetRandomSprite()
    {
        int spriteId = Random.Range(0, _configAsteroid.spritesList.Count);
        _spriteRenderer.sprite = _configAsteroid.spritesList[spriteId];
    }

    private void Explode()
    {
        _currentStage--;
        if (_currentStage <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Split();
        }
    }

    private void Split()
    {
        SetRandomSprite();
        transform.localScale = transform.localScale * 0.5f;
        _spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);
    }

    public override void OnHitTaken()
    {
        Explode();
    }
}
