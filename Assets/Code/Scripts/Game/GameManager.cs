using ServiceLocatorAsteroid.Service;
using UnityEngine;

public class GameManager : IGameService
{
    private const int MAX_LIVES = 3;
    [SerializeField] private Player _playerPrefab;

    private int _currentLives = MAX_LIVES;
    private Player _player;

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
        _player.Initialize(LoseLife);
        ServiceLocator.Current.Get<EnemyManager>().Initialize(RespawnEnemies);
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
            _player.Respawn();
        }
    }

    private void LoseGame()
    {
        Debug.LogError("MORREUUUUUUU");
    }

    private void RespawnEnemies()
    {
        EnemyManager manager = ServiceLocator.Current.Get<EnemyManager>();
        for (int i = 0; i < 5; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * 5f;
            manager.CreateEnemyAsteroid(spawnDirection, new Quaternion(0f,0f,0f,0f), 0);
        }
    }
}
