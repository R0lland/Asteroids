using ServiceLocatorAsteroid.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManagerService : IGameManagerService
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private UIGame _uiGamePrefab;

    private IEnemyService _enemyManager;
    private IUiService _uiManager;

    private Player _player;
    private ConfigGame _configGame;

    private int _currentLives;
    private int _numberOfAsteroidsToSpawn;
    private int _milestoneForNextLife;
    private int _score;

    public GameManagerService(Player player, ConfigGame configGame)
    {
        _playerPrefab = player;
        _configGame = configGame;
    }

    private void SpawnPlayer()
    {
        _player = GameObject.Instantiate(_playerPrefab, Vector3.zero, new Quaternion(0f, 0f, 0f, 0f));
    }

    public void Initialize()
    {
        ServiceLocator.Current.Get<IBulletService>().PoolObjects(20);
        SpawnPlayer();
        _score = 0;
        _numberOfAsteroidsToSpawn = _configGame.initialAsteroidsAmount;
        _currentLives = _configGame.maxLives;
        _milestoneForNextLife = _configGame.milestoneToNextLife;

        _enemyManager = ServiceLocator.Current.Get<IEnemyService>();
        _uiManager = ServiceLocator.Current.Get<IUiService>();

        _uiManager.CreateGameUI();
        _uiManager.GetGameUI().Initialize(_configGame.maxLives);
        _player.Initialize(LoseLife);
        _enemyManager.Initialize(SpawnAsteroids, Score);
        SpawnAsteroids();
    }

    private void LoseLife()
    {
        _currentLives--;
        _uiManager.GetGameUI().UpdateUI(_currentLives, _score);
        if (_currentLives <= 0)
        {
            WaitForSeconds(LoseGame, _configGame.timeToLose);
        }
        else
        {
            WaitForSeconds(RespawnPlayer, _configGame.timeToRespawn);
        }
    }

    private void RespawnPlayer()
    {
        _player.Respawn();
    }

    private void LoseGame()
    {
        _uiManager.GetGameUI().ShowFinalScoreScreen(_score, false);
        WaitForSeconds(ChangeState, _configGame.timeToLose);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_player.gameObject);
        ServiceLocator.Current.Get<IEnemyService>().DestroyAllEnemies();
        ServiceLocator.Current.Get<IBulletService>().DestroyAllBullets();
        _uiManager.RemoveCurrentUI();
        ServiceLocator.Current.Get<IStateService>().ChangeState(StateService.GameState.MENU);
    }

    private async void WaitForSeconds(Action onAsyncCompleted, int seconds)
    {
        await Task.Delay(seconds * 1000);
        onAsyncCompleted?.Invoke();
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < _numberOfAsteroidsToSpawn; i++)
        {
            Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * 5f;
            _enemyManager.CreateEnemyAsteroid(spawnDirection, new Quaternion(0f,0f,0f,0f), 0);
        }
        IncreaseNumberOfAsteroidsToSpawn(_configGame.additionalAsteroidsEachStage);
    }

    private void IncreaseNumberOfAsteroidsToSpawn(int addAsteroidsAmount)
    {
        _numberOfAsteroidsToSpawn += addAsteroidsAmount;
    }

    public void Score(int scoreValue)
    {
        _score += scoreValue;
        UpdateMilestone();
        _uiManager.GetGameUI().UpdateUI(_currentLives, _score);
        if (_score >= _configGame.scoreToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        _player.gameObject.SetActive(false);
        _uiManager.GetGameUI().ShowFinalScoreScreen(_score, true);
        WaitForSeconds(ChangeState, _configGame.timeToLose);
    }

    private void UpdateMilestone()
    {
        if (_score >= _milestoneForNextLife)
        {
            _currentLives++;
            _milestoneForNextLife += _configGame.milestoneToNextLife;
        }
    }
}
