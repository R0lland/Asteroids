using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeService : IHomeService
{
    private InputChecker _inputCheckerPrefab;

    private InputChecker _inputChecker;

    public HomeService(InputChecker inputCheckerPrefab) 
    {
        _inputCheckerPrefab = inputCheckerPrefab;
    }    

    public void Initialize()
    {
        _inputChecker = GameObject.Instantiate(_inputCheckerPrefab);
        ServiceLocator.Current.Get<IUiService>().CreateMenuUI();
        ServiceLocator.Current.Get<IEnemyService>().CreateEnemyAsteroids(6);
        _inputChecker.Initialize(ChangeState);
    }

    private void ChangeState()
    {
        GameObject.Destroy(_inputChecker.gameObject);
        ServiceLocator.Current.Get<IUiService>().RemoveCurrentUI();
        ServiceLocator.Current.Get<IEnemyService>().DestroyAllEnemies();
        ServiceLocator.Current.Get<IStateService>().ChangeState(StateService.GameState.GAMEPLAY);
    }
}
