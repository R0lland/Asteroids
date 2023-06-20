using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoaderService : IAssetLoaderService
{
    public void LoadAsset(AssetReference assetReference)
    {
        AsyncOperationHandle handle = assetReference.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;
    }

    private void Handle_Completed(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            
        }
        else
        {
            
        }
    }
}
