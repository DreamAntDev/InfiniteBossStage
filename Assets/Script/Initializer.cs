using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Addressables.InstantiateAsync("Assets/Prefab/Camera/CameraManager.prefab").Completed += OnLoadCamera;
    }

    private void OnLoadCamera(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Addressables.InstantiateAsync("Assets/Prefab/MainUI.prefab");
        Addressables.InstantiateAsync("Assets/Prefab/Lobby/Lobby.prefab");
    }
}
