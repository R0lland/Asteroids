using ServiceLocatorAsteroid.Service;
using static ViewService;

public interface IViewService : IGameService
{
    public void RemoveCurrentView();
    public View LoadView(ViewType viewType);
}