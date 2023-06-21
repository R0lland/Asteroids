using ServiceLocatorAsteroid.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetsService : IAssetsService
{
    private AssetReference _bullet;
    private AssetReference _asteroid;
    private AssetReference _player;
    private AssetReference _gameView;
    private AssetReference _homeView;
    private AssetReference _homeInput;

    private IAssetLoaderService _assetLoaderService;

    private List<AssetReference> _homePreloadAssets;
    private List<AssetReference> _gamePreloadAssets;

    public AssetsService(AssetReference bullet, AssetReference asteroid, AssetReference player, AssetReference gameView,
        AssetReference homeView, AssetReference homeInput, List<AssetReference> homePreloadAssets, List<AssetReference> gamePreloadAssets) {
        _bullet = bullet;
        _asteroid = asteroid;
        _player = player;
        _gameView = gameView;
        _homeView = homeView;
        _homeInput = homeInput;

        _homePreloadAssets = homePreloadAssets;
        _gamePreloadAssets = gamePreloadAssets;

        _assetLoaderService = ServiceLocator.Current.Get<IAssetLoaderService>();
    }

    public List<AssetReference> HomePreLoadAssets()
    {
        return _homePreloadAssets;
    }

    public List<AssetReference> GamePreLoadAssets()
    {
        return _gamePreloadAssets;
    }

    public Player GetPlayer()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_player);
        return gameObject.GetComponent<Player>();
    }

    public Asteroid GetAsteroid()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_asteroid);
        return gameObject.GetComponent<Asteroid>();
    }

    public Bullet GetBullet()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_bullet);
        return gameObject.GetComponent<Bullet>();
    }

    public GameView GetGameView()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_gameView);
        return gameObject.GetComponent<GameView>();
    }

    public HomeView GetHomeView()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_homeView);
        return gameObject.GetComponent<HomeView>();
    }

    public HomeInput GetHomeInput()
    {
        GameObject gameObject = _assetLoaderService.LoadAsset(_homeInput);
        return gameObject.GetComponent<HomeInput>();
    }
}
