using UnityEngine;
using ServiceLocatorAsteroid.Service;

public class GameInitializeServices : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private Saucer _saucer;
    [SerializeField] private Player _player;
    [SerializeField] private GameView _gameView;
    [SerializeField] private HomeView _homeView;
    [SerializeField] private ConfigGame _configGame;
    [SerializeField] private InputChecker _inputChecker;

    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register<IPoolingService>(new PoolingService());
        ServiceLocator.Current.Register<IBulletService>(new BulletService(_bullet));
        ServiceLocator.Current.Register<IEnemyService>(new EnemyService(_asteroid, _saucer));
        ServiceLocator.Current.Register<IViewService>(new ViewService(_gameView, _homeView));
        ServiceLocator.Current.Register<IStateService>(new StateService(_player, _configGame, _inputChecker));

        //Starts the game
        ServiceLocator.Current.Get<IStateService>().Initialize();
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Unregister<IPoolingService>();
        ServiceLocator.Current.Unregister<IBulletService>();
        ServiceLocator.Current.Unregister<IEnemyService>();
        ServiceLocator.Current.Unregister<IViewService>();
        ServiceLocator.Current.Unregister<IStateService>();
    }
}
