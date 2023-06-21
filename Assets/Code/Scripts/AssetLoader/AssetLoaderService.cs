using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoaderService : IAssetLoaderService
{
    private Dictionary<string, AsyncOperationHandle<GameObject>> _assets = new Dictionary<string, AsyncOperationHandle<GameObject>>();

    public GameObject LoadAsset(AssetReference assetReference)
    {
        AsyncOperationHandle<GameObject> asset = default;

        string key = assetReference.RuntimeKey as string;
        if (_assets.TryGetValue(key, out asset) == false)
        {
            Debug.LogError($"new asset requested, this should not happen. All assets were loaded at the start of the game");
            asset = Addressables.LoadAssetAsync<GameObject>(assetReference);
            _assets.Add(key, asset);
        }
        
        return asset.WaitForCompletion();
    }

    public void LoadAssets(List<AssetReference> assetReferenceList, Action onObjectsLoaded)
    {
        int numberOfAssetsToLoad = assetReferenceList.Count;
        foreach (AssetReference assetReference in assetReferenceList)
        {
            if (assetReference.RuntimeKeyIsValid() == false)
                continue;
            AsyncOperationHandle<GameObject> op = Addressables.LoadAssetAsync<GameObject>(assetReference);
            _assets[assetReference.RuntimeKey as string] = op;
            op.Completed += (operation) =>
            {
                numberOfAssetsToLoad--;
                
                if (numberOfAssetsToLoad <= 0)
                {
                    onObjectsLoaded?.Invoke();
                }
            };
        }
    }

    public void ReleaseAllHandles()
    {
        if (_assets.Count <= 0) return;
        foreach (var assetHandle in _assets.Values)
        {
            Addressables.Release(assetHandle);
        }
        _assets.Clear();
    }
}
