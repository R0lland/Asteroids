using UnityEngine;
using ServiceLocatorAsteroid.Service;

public class GameInitializeServices : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private Saucer _saucer;
    [SerializeField] private Player _player;
    [SerializeField] private UIGame _uiGame;
    [SerializeField] private UIMenu _uiMenu;
    [SerializeField] private ConfigGame _configGame;
    [SerializeField] private InputChecker _inputChecker;

    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register<IPoolingService>(new PoolingService());
        ServiceLocator.Current.Register<IBulletService>(new BulletService(_bullet));
        ServiceLocator.Current.Register<IEnemyService>(new EnemyService(_asteroid, _saucer));
        ServiceLocator.Current.Register<IUiService>(new UiService(_uiGame, _uiMenu));
        ServiceLocator.Current.Register<IHomeService>(new HomeService(_inputChecker));
        ServiceLocator.Current.Register<IGameManagerService>(new GameManagerService(_player, _configGame));
        ServiceLocator.Current.Register<IStateService>(new StateService());

        ServiceLocator.Current.Get<IStateService>().Initialize();
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Unregister<IPoolingService>();
        ServiceLocator.Current.Unregister<IBulletService>();
        ServiceLocator.Current.Unregister<IEnemyService>();
        ServiceLocator.Current.Unregister<IUiService>();
        ServiceLocator.Current.Unregister<IGameManagerService>();
        ServiceLocator.Current.Unregister<IHomeService>();
        ServiceLocator.Current.Unregister<IStateService>();
    }
}
