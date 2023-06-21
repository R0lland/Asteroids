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

    private GameView _viewGamePrefab;
    private HomeView _viewHomePrefab;

    private View _currentView;
    private Dictionary<ViewType, View> _viewTypes = new Dictionary<ViewType, View>();

    public ViewService(GameView _viewGame, HomeView viewHome)
    {
        _viewGamePrefab = _viewGame;
        _viewHomePrefab = viewHome;
        LoadUIs();
    }

    private void LoadUIs()
    {
        _viewTypes.Add(ViewType.Game, _viewGamePrefab);
        _viewTypes.Add(ViewType.Home, _viewHomePrefab);
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
        if(_viewTypes.TryGetValue(viewType, out View view))
        {
            View loadedView = GameObject.Instantiate(view);
            _currentView = loadedView;
            return loadedView;
        }
        return null;
    }
}
