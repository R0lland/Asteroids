using ServiceLocatorAsteroid.Service;
using System.Collections.Generic;
using UnityEngine;

public class ViewService : IViewService
{
    public enum ViewType
    {
        None,
        Home,
        Game
    }

    private View _currentView;

    private IAssetsService _assetsService;

    public ViewService()
    {
        _assetsService = ServiceLocator.Current.Get<IAssetsService>();
    }

    public void RemoveCurrentView()
    {
        if (_currentView)
        {
            GameObject.Destroy(_currentView.gameObject);
        }
    }

    public View LoadView(ViewType viewType)
    {
        View loadedView = null;
        if (viewType == ViewType.Home)
        {
            loadedView = GameObject.Instantiate(_assetsService.GetHomeView());
        } 
        else if (viewType == ViewType.Game)
        {
            loadedView = GameObject.Instantiate(_assetsService.GetGameView());
        }
        _currentView = loadedView;
        return loadedView;
    }
}
