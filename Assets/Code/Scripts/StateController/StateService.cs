using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class StateService : IStateService
{
    public enum GameState
    {
        HOME,
        GAMEPLAY
    }

    private ConfigGame _configGame;

    private HomeController _homeService;
    private GameController _gameController;

    private IEnemyService _enemyService;
    private IAssetLoaderService _assetLoaderService;

    private GameState _currentGameState;

    public StateService(ConfigGame configGame)
    {
        _configGame = configGame;

        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();
    }

    public void ChangeState(GameState newGameState)
    {
        CleanUp();
        _currentGameState = newGameState;
        WaitForMilliseconds(LoadNewState, 100);
        
    }

    private void LoadNewState()
    {
        switch (_currentGameState)
        {
            case GameState.HOME:
                InitializeHome();
                break;
            case GameState.GAMEPLAY:
                InitializeGame();
                break;
        }
    }

    private void CleanUp()
    {
        _homeService = null;
        _gameController = null;
        _assetLoaderService.ReleaseAllHandles();
    }

    private async void WaitForMilliseconds(Action onAsyncCompleted, int milliseconds)
    {
        await Task.Delay(milliseconds);
        onAsyncCompleted?.Invoke();
    }

    private void InitializeHome()
    {
        _homeService = new HomeController();
    }

    private void InitializeGame()
    {
        _gameController = new GameController(_configGame);
    }

    public void Initialize()
    {
        
        ChangeState(GameState.HOME);
    }
}
