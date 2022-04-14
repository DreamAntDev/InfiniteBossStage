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
<<<<<<< HEAD
        Addressables.InstantiateAsync("MainUI");
        Addressables.InstantiateAsync("Lobby");
=======
        UI.UILoader.Load("Assets/Prefab/MainUI.prefab");
        UI.UILoader.Load("Assets/Prefab/Lobby/Lobby.prefab");
        UI.UILoader.Load("Assets/Prefab/LoadingPage/LoadingPage.prefab");
>>>>>>> 769873fcc763060c7a9c3bbc89cb807212bfa15d
    }
}
