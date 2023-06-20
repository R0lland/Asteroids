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

    [SerializeField] private UIGame _uiGamePrefab;
    [SerializeField] private UIMenu _uiMenuPrefab;

    private UI _currentUI;
    private Dictionary<UIType, UI> _uiTypes = new Dictionary<UIType, UI>();

    public UiService(UIGame _uiGame, UIMenu uiMenu)
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

    public UI LoadUI(UIType uiType)
    {
        if(_uiTypes.TryGetValue(uiType, out UI ui))
        {
            UI loadedUI = GameObject.Instantiate(ui);
            return loadedUI;
        }
        return null;
    }
}
