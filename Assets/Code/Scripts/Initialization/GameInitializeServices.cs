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

        ServiceLocator.Current.Register(new Pooling());
        ServiceLocator.Current.Register(new BulletManager(_bullet));
        ServiceLocator.Current.Register(new EnemyManager(_asteroid, _saucer));
        ServiceLocator.Current.Register(new GameManager(_player, _uiGame));
    }
}
