using ServiceLocatorAsteroid.Service;

public class StateService : IStateService
{
    public enum GameState
    {
        MENU,
        GAMEPLAY
    }

    private Player _player;
    private ConfigGame _configGame;
    private InputChecker _inputChecker;

    private HomeController _homeService;
    private GameController _gameController;

    public StateService(Player player, ConfigGame configGame, InputChecker inputChecker)
    {
        _player = player;
        _configGame = configGame;
        _inputChecker = inputChecker;
    }

    public void ChangeState(GameState newGameState)
    {
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
        _homeService = new HomeController(_inputChecker);
    }

    private void InitializeGame()
    {
        _gameController = new GameController(_player, _configGame);
    }

    public void Initialize()
    {
        ServiceLocator.Current.Get<IEnemyService>().PoolObjects(20);
        ChangeState(GameState.MENU);
    }
}
