using UnityEngine;
using ServiceLocatorAsteroid.Service;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

public class GameInitializeServices : MonoBehaviour
{
    [SerializeField] private AssetReference _bullet;
    [SerializeField] private AssetReference _asteroid;
    [SerializeField] private AssetReference _player;
    [SerializeField] private AssetReference _gameView;
    [SerializeField] private AssetReference _homeView;
    [SerializeField] private AssetReference _homeInput;

    [SerializeField] private ConfigGame _configGame;

    [SerializeField] private List<AssetReference> _homePreloadAssets;
    [SerializeField] private List<AssetReference> _gamePreloadAssets;

    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register<IPoolingService>(new PoolingService());
        ServiceLocator.Current.Register<IAssetLoaderService>(new AssetLoaderService());
        ServiceLocator.Current.Register<IAssetsService>(new AssetsService(_bullet, _asteroid, _player, _gameView, _homeView, _homeInput, _homePreloadAssets, _gamePreloadAssets));
        ServiceLocator.Current.Register<IBulletService>(new BulletService());
        ServiceLocator.Current.Register<IEnemyService>(new EnemyService());
        ServiceLocator.Current.Register<IViewService>(new ViewService());
        ServiceLocator.Current.Register<IStateService>(new StateService(_configGame));

        //Starts the game
        ServiceLocator.Current.Get<IStateService>().Initialize();
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Unregister<IPoolingService>();
        ServiceLocator.Current.Unregister<IAssetLoaderService>();
        ServiceLocator.Current.Unregister<IBulletService>();
        ServiceLocator.Current.Unregister<IEnemyService>();
        ServiceLocator.Current.Unregister<IViewService>();
        ServiceLocator.Current.Unregister<IStateService>();
    }
}
