using UnityEngine;
using ServiceLocatorAsteroid.Service;

public class GameInitializeServices : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private Saucer _saucer;
    [SerializeField] private Player _player;
    [SerializeField] private UIGame _uiGame;

    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register<IPoolingService>(new Pooling());
        ServiceLocator.Current.Register<IBulletManager>(new BulletManager(_bullet));
        ServiceLocator.Current.Register<IEnemyManager>(new EnemyManager(_asteroid, _saucer));
        ServiceLocator.Current.Register<IUIManager>(new UIManager(_uiGame));
        ServiceLocator.Current.Register<IGameManager>(new GameManager(_player));
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Unregister<IPoolingService>();
        ServiceLocator.Current.Unregister<IBulletManager>();
        ServiceLocator.Current.Unregister<IEnemyManager>();
        ServiceLocator.Current.Unregister<IGameManager>();
    }
}
