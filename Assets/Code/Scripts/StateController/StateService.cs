using ServiceLocatorAsteroid.Service;

public class StateService : IStateService
{
    public enum GameState
    {
        MENU,
        GAMEPLAY
    }

    public void ChangeState(GameState newGameState)
    {
        ServiceLocator.Current.Get<IUiService>().RemoveCurrentUI();
        switch (newGameState)
        {
            case GameState.MENU:
                InitializeMenu();
                break; 
            case GameState.GAMEPLAY:
                InitializeGame();
                break;
        }
    }

    private void InitializeMenu()
    {
        ServiceLocator.Current.Get<IHomeService>().Initialize();
    }

    private void InitializeGame()
    {
        ServiceLocator.Current.Get<IGameManagerService>().Initialize();
    }

    public void Initialize()
    {
        ChangeState(GameState.MENU);
    }
}
