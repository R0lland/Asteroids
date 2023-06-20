using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController
{
    private InputChecker _inputCheckerPrefab;

    private IUiService _uiService;
    private IEnemyService _enemyService;
    private IStateService _stateService;

    private InputChecker _inputChecker;

    public HomeController(InputChecker inputCheckerPrefab) 
    {
        _inputCheckerPrefab = inputCheckerPrefab;

        _uiService = ServiceLocator.Current.Get<IUiService>();
        _enemyService = ServiceLocator.Current.Get<IEnemyService>();
        _stateService = ServiceLocator.Current.Get<IStateService>();
        Initialize();
    }    

    public void Initialize()
    {
        _inputChecker = GameObject.Instantiate(_inputCheckerPrefab);
        _uiService.LoadUI(UiService.UIType.Menu);
        _enemyService.CreateEnemyAsteroids(6);
        _inputChecker.Initialize(ChangeState);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_inputChecker.gameObject);
        _uiService.RemoveCurrentUI();
        _enemyService.DestroyAllEnemies();
        _stateService.ChangeState(StateService.GameState.GAMEPLAY);
    }
}
