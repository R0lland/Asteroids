using ServiceLocatorAsteroid.Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAssetLoaderService : IGameService
{
    public void LoadAssets(List<AssetReference> assetReferenceList, Action onObjectsLoaded);
    public GameObject LoadAsset(AssetReference assetReference);
    public void ReleaseAllHandles();
}
