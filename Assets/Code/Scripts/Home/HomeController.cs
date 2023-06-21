using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HomeController
{
    private AssetReference _inputCheckerPrefab;

    private IViewService _viewService;
    private IEnemyService _enemyService;
    private IStateService _stateService;
    private IAssetLoaderService _assetLoaderService;

    private GameObject _inputChecker;
    private HomeView _homeView;

    public HomeController(AssetReference inputCheckerPrefab, List<AssetReference> preloadAssets) 
    {
        _inputCheckerPrefab = inputCheckerPrefab;

        _viewService = ServiceLocator.Current.Get<IViewService>();
        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _stateService = ServiceLocator.Current.Get<IStateService>();
        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();

        _assetLoaderService.LoadAssets(preloadAssets, Initialize);
    }    

    public void Initialize()
    {
        _inputChecker = GameObject.Instantiate(_assetLoaderService.LoadAsset(_inputCheckerPrefab));
        View view = _viewService.LoadView(ViewService.ViewType.Home);
        _homeView = view.GetComponent<HomeView>();
        _enemyService.CreateEnemyAsteroids(6);
        _inputChecker.GetComponent<InputChecker>().Initialize(ChangeState);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_inputChecker.gameObject);
        _viewService.RemoveCurrentView();
        _enemyService.DestroyAllEnemies();
        _stateService.ChangeState(StateService.GameState.GAMEPLAY);
    }
}
