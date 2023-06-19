using UnityEngine;

public class UiService : IUiService
{
    [SerializeField] private UIGame _uiGamePrefab;

    private UIGame _uiGame;

    public UiService(UIGame _uiGame)
    {
        _uiGamePrefab = _uiGame;
    }

    public void CreateGameUI()
    {
        if (_uiGame == null)
        {
            _uiGame = GameObject.Instantiate(_uiGamePrefab);
        }
    }

    public UIGame GetGameUI()
    {
        return _uiGame;
    }
}
