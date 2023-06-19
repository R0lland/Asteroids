using UnityEngine;
using ServiceLocatorAsteroid.Service;

public class GameInitializeServices : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Asteroid _asteroid;
    [SerializeField] private Player _player;
    [SerializeField] private UIGame _uiGame;

    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register(new SpawnManager());
        ServiceLocator.Current.Register(new BulletManager(_bullet));
        ServiceLocator.Current.Register(new EnemyManager(_asteroid));
        
    }

    private void Start()
    {
        ServiceLocator.Current.Register(new GameManager(_player, _uiGame));
    }
}
