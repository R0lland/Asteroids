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

    private Player _player;
    private ConfigGame _configGame;
    private AssetReference _inputChecker;

    private HomeController _homeService;
    private GameController _gameController;
    private List<AssetReference> _preloadAssetsHome;

    private IEnemyService _enemyService;
    private IAssetLoaderService _assetLoaderService;

    public StateService(Player player, ConfigGame configGame, AssetReference inputChecker, List<AssetReference> preloadAssetsHome)
    {
        _player = player;
        _configGame = configGame;
        _inputChecker = inputChecker;
        _preloadAssetsHome = preloadAssetsHome;

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
        _homeService = new HomeController(_inputChecker, _preloadAssetsHome);
    }

    private void InitializeGame()
    {
        _gameController = new GameController(_player, _configGame);
    }

    public void Initialize()
    {
        _enemyService.PoolObjects(20);
        ChangeState(GameState.HOME);
    }
}
