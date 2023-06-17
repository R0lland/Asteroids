using UnityEngine;
using ServiceLocatorAsteroid.Service;

public class GameInitializeServices : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.Initialize();

        ServiceLocator.Current.Register(new GameManager());
        ServiceLocator.Current.Register(new SpawnManager());
    }
}
