using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HomeController
{
    private IViewService _viewService;
    private IEnemyService _enemyService;
    private IStateService _stateService;
    private IAssetLoaderService _assetLoaderService;
    private IAssetsService _assetsService;

    private HomeInput _homeInput;
    private HomeView _homeView;

    public HomeController() 
    {
        _viewService = ServiceLocator.Current.Get<IViewService>();
        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _stateService = ServiceLocator.Current.Get<IStateService>();
        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();
        _assetsService = ServiceLocator.Current.Get<IAssetsService>();

        _assetLoaderService.LoadAssets(_assetsService.HomePreLoadAssets(), Initialize);
    }    

    public void Initialize()
    {
        _homeInput = GameObject.Instantiate(_assetsService.GetHomeInput());
        _enemyService.PoolObjects(20);
        View view = _viewService.LoadView(ViewService.ViewType.Home);
        _homeView = view.GetComponent<HomeView>();
        _enemyService.CreateEnemyAsteroids(6);
        _homeInput.Initialize(ChangeState);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_homeInput.gameObject);
        _viewService.RemoveCurrentView();
        _enemyService.DestroyAllEnemies();
        _stateService.ChangeState(StateService.GameState.GAMEPLAY);
    }
}
