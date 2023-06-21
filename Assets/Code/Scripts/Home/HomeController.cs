using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController
{
    private InputChecker _inputCheckerPrefab;

    private IViewService _viewService;
    private IEnemyService _enemyService;
    private IStateService _stateService;

    private InputChecker _inputChecker;
    private HomeView _homeView;

    public HomeController(InputChecker inputCheckerPrefab) 
    {
        _inputCheckerPrefab = inputCheckerPrefab;

        _viewService = ServiceLocator.Current.Get<IViewService>();
        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _stateService = ServiceLocator.Current.Get<IStateService>();
        Initialize();
    }    

    public void Initialize()
    {
        _inputChecker = GameObject.Instantiate(_inputCheckerPrefab);
        View view = _viewService.LoadView(ViewService.ViewType.Home);
        _homeView = view.GetComponent<HomeView>();
        _enemyService.CreateEnemyAsteroids(6);
        _inputChecker.Initialize(ChangeState);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_inputChecker.gameObject);
        _viewService.RemoveCurrentView();
        _enemyService.DestroyAllEnemies();
        _stateService.ChangeState(StateService.GameState.GAMEPLAY);
    }
}
