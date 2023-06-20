using ServiceLocatorAsteroid.Service;
using static UiService;

public interface IUiService : IGameService
{
    public void RemoveCurrentUI();
    public View LoadUI(UIType uiType);
}