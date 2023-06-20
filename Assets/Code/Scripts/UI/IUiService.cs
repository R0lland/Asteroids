using ServiceLocatorAsteroid.Service;
using static UiService;

public interface IUiService : IGameService
{
    public void RemoveCurrentUI();
    public UI LoadUI(UIType uiType);
}