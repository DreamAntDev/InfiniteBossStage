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
        UI.UILoader.Load("Assets/Prefab/MainUI.prefab");
        UI.UILoader.Load("Assets/Prefab/Lobby/Lobby.prefab");
        UI.UILoader.Load("Assets/Prefab/LoadingPage/LoadingPage.prefab");
    }
}
