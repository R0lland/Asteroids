using System.Collections.Generic;
using UnityEngine;

public class UiService : IUiService
{
    public enum UIType
    {
        None,
        Menu,
        Game
    }

    [SerializeField] private GameView _uiGamePrefab;
    [SerializeField] private HomeView _uiMenuPrefab;

    private View _currentUI;
    private Dictionary<UIType, View> _uiTypes = new Dictionary<UIType, View>();

    public UiService(GameView _uiGame, HomeView uiMenu)
    {
        _uiGamePrefab = _uiGame;
        _uiMenuPrefab = uiMenu;
        LoadUIs();
    }

    private void LoadUIs()
    {
        _uiTypes.Add(UIType.Game, _uiGamePrefab);
        _uiTypes.Add(UIType.Menu, _uiMenuPrefab);
    }

    public void RemoveCurrentUI()
    {
        if (_currentUI)
        {
            GameObject.Destroy(_currentUI.gameObject);
        }
    }

    public View LoadUI(UIType uiType)
    {
        if(_uiTypes.TryGetValue(uiType, out View ui))
        {
            View loadedUI = GameObject.Instantiate(ui);
            return loadedUI;
        }
        return null;
    }
}
