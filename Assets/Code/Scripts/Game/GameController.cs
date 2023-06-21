using ServiceLocatorAsteroid.Service;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameController
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameView _viewGamePrefab;

    private IEnemyService _enemyService;
    private IViewService _viewService;
    private IStateService _stateService;
    private IBulletService _bulletService;
    private IAssetLoaderService _assetLoaderService;
    private IAssetsService _assetsService;

    private Player _player;
    private ConfigGame _configGame;
    private GameView _gameView;

    private int _currentLives;
    private int _numberOfAsteroidsToSpawn;
    private int _milestoneForNextLife;
    private int _score;

    public GameController(ConfigGame configGame)
    {
        _configGame = configGame;

        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _viewService = ServiceLocator.Current.Get<IViewService>();
        _stateService = ServiceLocator.Current.Get<IStateService>();
        _bulletService = ServiceLocator.Current.Get<IBulletService>();
        _assetsService = ServiceLocator.Current.Get<IAssetsService>();
        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();

        _assetLoaderService.LoadAssets(_assetsService.GamePreLoadAssets(), Initialize);
    }

    private void SpawnPlayer()
    {
        _player = GameObject.Instantiate(_assetsService.GetPlayer());
    }

    public void Initialize()
    {
        _score = 0;
        _numberOfAsteroidsToSpawn = _configGame.initialAsteroidsAmount;
        _currentLives = _configGame.maxLives;
        _milestoneForNextLife = _configGame.milestoneToNextLife;
        SpawnPlayer();
        InitializeServices();
        SpawnAsteroids();
    }

    private void InitializeServices()
    {
        _bulletService.PoolObjects(20);
        View loadedView = _viewService.LoadView(ViewService.ViewType.Game);
        _gameView = loadedView.GetComponent<GameView>();
        _gameView.Initialize(_configGame.maxLives);
        _player.Initialize(LoseLife);
        _enemyService.Initialize(SpawnAsteroids, Score);
    }

    private void LoseLife()
    {
        _currentLives--;
        _gameView.UpdateView(_currentLives, _score);
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
        _gameView.ShowFinalScoreScreen(_score, false);
        WaitForSeconds(ChangeState, _configGame.timeToLose);
    }

    private void DestroyPlayer()
    {
        _player.ShutDown();
        GameObject.Destroy(_player.gameObject);
    }

    private void ActivatePlayer(bool activate)
    {
        _player.Activate(activate);
    }

    private void ChangeState()
    {
        DestroyPlayer();
        _viewService.RemoveCurrentView();
        _enemyService.DestroyAllEnemies();
        _bulletService.DestroyAllBullets();
        _stateService.ChangeState(StateService.GameState.HOME);
    }

    private async void WaitForSeconds(Action onAsyncCompleted, int seconds)
    {
        await Task.Delay(seconds * 1000);
        onAsyncCompleted?.Invoke();
    }

    private void SpawnAsteroids()
    {
        _enemyService.CreateEnemyAsteroids(_numberOfAsteroidsToSpawn);
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
        _gameView.UpdateView(_currentLives, _score);
        if (_score >= _configGame.scoreToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        ActivatePlayer(false);
        _gameView.ShowFinalScoreScreen(_score, true);
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
