using ServiceLocatorAsteroid.Service;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

public interface IAssetsService : IGameService
{
    public List<AssetReference> HomePreLoadAssets();

    public List<AssetReference> GamePreLoadAssets();
    public Player GetPlayer();

    public Asteroid GetAsteroid();

    public Bullet GetBullet();

    public GameView GetGameView();

    public HomeView GetHomeView();

    public HomeInput GetHomeInput();
}
