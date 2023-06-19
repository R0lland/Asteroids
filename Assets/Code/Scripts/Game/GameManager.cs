using ServiceLocatorAsteroid.Service;
using System;
using UnityEngine;

public class GameManager : IGameManager
{
    private const int MAX_LIVES = 3;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private UIGame _uiGamePrefab;

    private IEnemyManager _enemyManager;
    private IUIManager _uiManager;

    private int _currentLives = MAX_LIVES;
    private Player _player;
    private int _score;

    public GameManager(Player player)
    {
        _playerPrefab = player;
        SpawnPlayer();
        Initialize();

    }

    private void SpawnPlayer()
    {
        _player = GameObject.Instantiate(_playerPrefab, Vector3.zero, new Quaternion(0f, 0f, 0f, 0f));
    }

    public void Initialize()
    {
        _score = 0;
        _enemyManager = ServiceLocator.Current.Get<IEnemyManager>();
        _uiManager = ServiceLocator.Current.Get<IUIManager>();
        _uiManager.CreateGameUI();
        _uiManager.GetGameUI().Initialize(MAX_LIVES);
        _player.Initialize(LoseLife);
        _enemyManager.Initialize(RespawnEnemies, Score);
        RespawnEnemies();
    }

    private void LoseLife()
    {
        _currentLives--;
        if (_currentLives <= 0)
        {
            LoseGame();
        }
        else
        {
            _uiManager.GetGameUI().UpdateUI(_currentLives, _score);
            _player.Respawn();
        }
    }

    private void LoseGame()
    {

    }

    private void RespawnEnemies()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * 5f;
            _enemyManager.CreateEnemyAsteroid(spawnDirection, new Quaternion(0f,0f,0f,0f), 0);
        }
    }

    public void Score(int scoreValue)
    {
        _score += scoreValue;
        _uiManager.GetGameUI().UpdateUI(_currentLives, _score);
    }
}
