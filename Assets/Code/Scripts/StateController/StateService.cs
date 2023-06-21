using ServiceLocatorAsteroid.Service;
using System.Collections.Generic;
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

    public StateService(ConfigGame configGame)
    {
        _configGame = configGame;

        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();
    }

    public void ChangeState(GameState newGameState)
    {
        _homeService = null;
        _gameController = null;
        _assetLoaderService.ReleaseAllHandles();
        switch (newGameState)
        {
            case GameState.HOME:
                InitializeHome();
                break; 
            case GameState.GAMEPLAY:
                InitializeGame();
                break;
        }
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
