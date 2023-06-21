using ServiceLocatorAsteroid.Service;

public class StateService : IStateService
{
    public enum GameState
    {
        HOME,
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
        _homeService = null;
        _gameController = null;
        switch (newGameState)
        {
            case GameState.HOME:
                InitializeHome();
                break; 
            case GameState.GAMEPLAY:
                InitializeGame();
                break;
        }
    }

    private void InitializeHome()
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
        ChangeState(GameState.HOME);
    }
}
