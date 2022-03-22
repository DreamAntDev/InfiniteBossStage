using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AI;

using System.Linq;
namespace Static
{
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
        System.Action callback = null;
        // 임시로 게임오브젝트로 설정
        GameObject boss;
        GameObject character;
        GameObject world;
        AsyncOperationHandle<SceneInstance> currentWorldHandle;
        Data.Stage.Stage curData = null;


        bool isLoading = false;
        public void Awake()
        {
            if (instance != null)
                Destroy(instance);

            instance = this;
        }

        public void Update()
        {
            if (this.isLoading == true && this.boss != null && this.character != null && this.world != null)
            {
                LoadSuccess();
            }
        }
        private void LoadSuccess()
        {
            SetObjectPosition();
            callback?.Invoke();
            callback = null;

            Static.CameraManager.Instance.LobbyCamera.gameObject.SetActive(false);
            Static.CameraManager.Instance.MainCamera.gameObject.SetActive(true);
            Static.CameraManager.Instance.UICamera.gameObject.SetActive(true);

            Static.CameraManager.Instance.MainCamera.GetComponent<Component.TargetTraceCamera>().target = this.character.transform;

            this.isLoading = false;
        }
        public void LoadStage(int index, System.Action loadCallback = null)
        {
            if (this.isLoading == true)
                return;

            callback = loadCallback;
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
            InitStage();
            curData = data;
            string world = data.WorldPrefab;

            this.isLoading = true;
            Addressables.LoadSceneAsync(world, LoadSceneMode.Additive, true).Completed += StageManager_WorldComplete;
            //Addressables.InstantiateAsync(world).Completed += StageManager_WorldComplete;
        }

        private void StageManager_WorldComplete(AsyncOperationHandle<SceneInstance> obj)
        {
            currentWorldHandle = obj;
            foreach(var root in obj.Result.Scene.GetRootGameObjects())
            {
                if(root.GetComponentsInChildren<MapComponent.SpawnPoint>().Length > 0)
                {
                    this.world = root;
                    break;
                }
            }

            string boss = curData.BossPrefab;

            //임시 캐릭터 경로
            string character = "Assets/Prefab/DogPBR.prefab";

            Addressables.InstantiateAsync(boss).Completed += StageManager_BossComplete;
            Addressables.InstantiateAsync(character).Completed += StageManager_CharacterComplete;
        }

        private void InitStage()
        {
            if (this.boss != null)
                GameObject.Destroy(this.boss);
            if (this.world != null)
                GameObject.Destroy(this.world);
            if (this.character != null)
                GameObject.Destroy(this.character);

            this.boss = null;
            this.character = null;
            this.world = null;

            if (this.currentWorldHandle.IsValid())
            {
                Addressables.UnloadSceneAsync(this.currentWorldHandle, true);
            }
        }

        private void StageManager_DataCompleted(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Data.Stage.Stage> obj)
        {
            stageDataTree.Add(obj.Result.Index, obj.Result);
            LoadStage(obj.Result);
        }

        private void StageManager_BossComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            this.boss = obj.Result;
        }
        private void StageManager_CharacterComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            this.character = obj.Result;
        }
        private void SetObjectPosition()
        {
            this.world.transform.position = Vector3.zero;
            var spawnPointList = this.world.GetComponentsInChildren<MapComponent.SpawnPoint>();
            foreach (var point in spawnPointList)
            {
                if (point.eSpawnType == MapComponent.SpawnPoint.SpawnType.Boss)
                {
                    //this.boss.transform.position = point.transform.position;
                    NavMeshHit closestHit;
                    if (NavMesh.SamplePosition(point.transform.position, out closestHit, 500, 1))
                    {
                        this.boss.transform.position = closestHit.position;
                        var navMeshAgent = this.boss.GetComponent<NavMeshAgent>();
                        navMeshAgent.enabled = false;
                        navMeshAgent.enabled = true;
                    }
                    else
                    {
                        Debug.LogError("NavMesh Create Fail");
                    }
                }
                else if (point.eSpawnType == MapComponent.SpawnPoint.SpawnType.Character)
                {
                    this.character.GetComponent<CharacterController>().enabled = false;
                    this.character.transform.position = point.transform.position;
                    this.character.GetComponent<CharacterController>().enabled = true;
                }
            }
        }
    }
}