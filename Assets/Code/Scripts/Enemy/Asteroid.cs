using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Enemy
{
    [SerializeField] ConfigAsteroidSprites _configAsteroid;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<ConfigAsteroidStage> _asteroidStages = new List<ConfigAsteroidStage>();

    private ConfigAsteroidStage _currentStage;

    public override void Initialize(Action<int> onEnemyDestroyed)
    {
        base.Initialize(onEnemyDestroyed);
        SetRandomSprite();
        SetDirection();
    }

    public void SetAsteroidStage(int stage)
    {
        if (stage > _asteroidStages.Count - 1 || stage < 0)
        {
            Debug.LogError("Number of asteroid stages cannot be less then zero or higher then the number of total stages");
            _currentStage = _asteroidStages[0];
            return;
        }
        _currentStage = _asteroidStages[stage];
        _scoreValue = _currentStage.scoreValue;
        SetStageData();
    }

    private void SetStageData()
    {
        _speed = UnityEngine.Random.Range(_currentStage.minSpeed, _currentStage.maxSpeed);
        transform.localScale = new Vector3(_currentStage.size, _currentStage.size, _currentStage.size);
        _spriteRenderer.transform.eulerAngles = new Vector3(0f, 0f, UnityEngine.Random.value * 360);
    }

    private void SetRandomSprite()
    {
        int spriteId = UnityEngine.Random.Range(0, _configAsteroid.spritesList.Count);
        _spriteRenderer.sprite = _configAsteroid.spritesList[spriteId];
    }

    protected override void Explode()
    {
        int nextStage = _currentStage.id + 1;
        if (nextStage >= _asteroidStages.Count)
        {
            _enemyManager.RemoveEnemy(this);
        }
        else
        {
            Split(nextStage);
        }
    }

    private void Split(int nextStage)
    {
        SetAsteroidStage(nextStage);
        SetRandomSprite();
        SetDirection();
        SetStageData();
        if (_enemyManager != null)
        {
            for (int i = 0; i < _currentStage.additionalAsteroid; i++)
            {
                _enemyManager.CreateEnemyAsteroid(transform.position, transform.rotation, nextStage);
            }
        }
    }
}
