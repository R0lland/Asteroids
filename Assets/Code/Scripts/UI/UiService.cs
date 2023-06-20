using UnityEngine;

public class UiService : IUiService
{
    [SerializeField] private UIGame _uiGamePrefab;
    [SerializeField] private UIMenu _uiMenuPrefab;

    private UIGame _uiGame;
    private UIMenu _uiMenu;

    private UI _currentUI;

    public UiService(UIGame _uiGame, UIMenu uiMenu)
    {
        _uiGamePrefab = _uiGame;
        _uiMenuPrefab = uiMenu;
    }

    public void RemoveCurrentUI()
    {
        if (_currentUI)
        {
            GameObject.Destroy(_currentUI.gameObject);
        }
    }

    public void CreateGameUI()
    {
        if (_uiGame == null)
        {
            _uiGame = GameObject.Instantiate(_uiGamePrefab);
            _currentUI = _uiGame;
        }
    }

    public void CreateMenuUI()
    {
        if (_uiMenu == null)
        {
            _uiMenu = GameObject.Instantiate(_uiMenuPrefab);
            _currentUI = _uiMenu;
        }
    }

    public UIGame GetGameUI()
    {
        return _uiGame;
    }
}
