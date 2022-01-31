using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StageManager : MonoBehaviour
{
    Dictionary<int, Data.Stage.Stage> stageDataTree = new Dictionary<int, Data.Stage.Stage>();
    static StageManager instance = null;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.AddComponent<StageManager>();
            }
            return instance;
        }
    }
    public void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;
    }

    public void LoadStage(int index=1)
    {
        if (stageDataTree.ContainsKey(index) == false)
        {
            Addressables.LoadAssetAsync<Data.Stage.Stage>("Assets/Data/Stage/Stage1.asset").Completed += StageManager_DataCompleted;
        }
        else
        {
            LoadStage(stageDataTree[index]);
        }
    }
    private void LoadStage(Data.Stage.Stage data)
    {
        string world = data.WorldPrefab;
        string boss = data.BossPrefab;

        Addressables.InstantiateAsync(world).Completed += StageManager_WorldComplete;
        Addressables.InstantiateAsync(boss).Completed += StageManager_BossComplete;
    }
    private void StageManager_DataCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Data.Stage.Stage> obj)
    {
        stageDataTree.Add(obj.Result.Index, obj.Result);
        LoadStage(obj.Result);
    }

    private void StageManager_WorldComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("TestWorld");
    }
    private void StageManager_BossComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("TestWorld2");
    }
}
