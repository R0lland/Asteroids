using ServiceLocatorAsteroid.Service;
using System;
using UnityEngine;

public class GameManager : IGameManager
{
    private const int MAX_LIVES = 3;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private UIGame _uiGamePrefab;

    private int _currentLives = MAX_LIVES;
    private Player _player;
    private UIGame _uiGame;
    private int _score;
    private Action<int, int> _onUiUpdate;

    public GameManager(Player player)
    {
        _playerPrefab = player;
        SpawnPlayer();
        Initialize();

    }

    private void SpawnPlayer()
    {
        _player = GameObject.Instantiate(_playerPrefab, Vector3.zero, new Quaternion(0f, 0f, 0f, 0f));
        _uiGame = GameObject.Instantiate(_uiGamePrefab);
    }

    public void Initialize()
    {
        _score = 0;
        _player.Initialize(LoseLife);
        _uiGame.Initialize(MAX_LIVES);
        _onUiUpdate = _uiGame.UpdateUI;
        ServiceLocator.Current.Get<IEnemyManager>().Initialize(RespawnEnemies);
        RespawnEnemies();
    }

    private void LoseLife()
    {
        Debug.LogError("LoseLife");
        _currentLives--;
        if (_currentLives <= 0)
        {
            LoseGame();
        }
        else
        {
            _onUiUpdate?.Invoke(_currentLives, _score);
            _player.Respawn();
        }
    }

    private void LoseGame()
    {
        Debug.LogError("MORREUUUUUUU");
    }

    private void RespawnEnemies()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * 5f;
            ServiceLocator.Current.Get<IEnemyManager>().CreateEnemyAsteroid(spawnDirection, new Quaternion(0f,0f,0f,0f), 0);
        }
    }

    public void Score(int scoreValue)
    {
        _score += scoreValue;
        _onUiUpdate?.Invoke(_currentLives, _score);
    }
}
