using ServiceLocatorAsteroid.Service;
using UnityEngine.AddressableAssets;

public interface IAssetLoaderService : IGameService
{
    public void LoadAsset(AssetReference assetReference);
}
