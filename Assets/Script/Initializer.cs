using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Addressables.InstantiateAsync("CameraManager").Completed += OnLoadCamera;
    }

    private void OnLoadCamera(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Addressables.InstantiateAsync("MainUI");
        Addressables.InstantiateAsync("Lobby");
        UI.UILoader.Load("MainUI");
        UI.UILoader.Load("Lobby");
        UI.UILoader.Load("LoadingPage");
    }
}
