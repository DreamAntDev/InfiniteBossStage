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
        enum State
        {
            Idle,
            DataLoading,
            Loading,
            LoadSuccess,
        }

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
        AudioClip bgmClip;
        AsyncOperationHandle<SceneInstance> currentWorldHandle;

        List<AsyncOperationHandle> loadDataList = new List<AsyncOperationHandle>();
        List<AsyncOperationHandle> loadList = new List<AsyncOperationHandle>();
        UI.LoadingPage loadingPage = null;
        State state
        {
            get
            {
                if (this.loadDataList.Count > 0)
                    return State.DataLoading;

                if (this.loadList.Count == 0)
                    return State.Idle;

                var waitIdx = this.loadList.FindIndex(o => o.Status == AsyncOperationStatus.None);
                if (waitIdx >= 0)
                    return State.Loading;

                return State.LoadSuccess;
            }
        }
        public void Awake()
        {
            if (instance != null)
                Destroy(instance);

            instance = this;
        }

        public void Update()
        {
            if (state == State.LoadSuccess && this.loadingPage != null && this.loadingPage.IsLoading() == false)
            {
                this.loadingPage = null;
                LoadSuccess();
            }

            if (UnityEngine.InputSystem.Keyboard.current.xKey.wasPressedThisFrame)
            {
                UnloadStage();
            }
        }

        public void UnloadStage()
        {
            InitStage();
            Static.CameraManager.Instance.LobbyCamera.gameObject.SetActive(true);
            Static.CameraManager.Instance.MainCamera.gameObject.SetActive(false);
            Static.CameraManager.Instance.UICamera.gameObject.SetActive(false);

            Static.CameraManager.Instance.MainCamera.GetComponent<Component.TargetTraceCamera>().target = null;
        }

        private void LoadSuccess()
        {
            this.loadList.Clear();
            if (this.bgmClip != null)
            {
                Static.SoundManager.Instance.PlaySound(this.bgmClip, SoundManager.SoundType.BGM);
            }
            SetObjectPosition();

            callback?.Invoke();
            callback = null;

            Static.CameraManager.Instance.LobbyCamera.gameObject.SetActive(false);
            Static.CameraManager.Instance.MainCamera.gameObject.SetActive(true);
            Static.CameraManager.Instance.UICamera.gameObject.SetActive(true);

            Static.CameraManager.Instance.MainCamera.GetComponent<Component.TargetTraceCamera>().target = this.character.transform;
        }
        public void LoadStage(int index)
        {
            if (state != State.Idle)
                return;

            var loading = UI.UILoader.GetUI<UI.LoadingPage>("LoadingPage");
            if (loading != null)
            {
                var data = new UI.LoadingPage.FadeData(
                    () => LoadStage(Data.Stage.Instance.Container[index]),
                    () => { return this.state != State.Loading; }
                    );
                this.loadingPage = loading;
                this.loadingPage.SetLoading(data);
            }
            //LoadStage(Data.Stage.Instance.Container[index]);
        }

        private void LoadStage(Data.Stage.Stage data)
        {
            InitStage();
            if (this.loadDataList.Count > 0)
                this.loadDataList.Clear();

            string world = data.WorldPrefab;
            string boss = data.BossPrefab;
            string character = "DogPBR";

            currentWorldHandle = Addressables.LoadSceneAsync(world, LoadSceneMode.Additive, true);
            currentWorldHandle.Completed += StageManager_WorldComplete;
            this.loadList.Add(currentWorldHandle);

            var bossHandle = Addressables.InstantiateAsync(boss);
            bossHandle.Completed += StageManager_BossComplete;
            this.loadList.Add(bossHandle);

            var characterHandle = Addressables.InstantiateAsync(character);
            characterHandle.Completed += StageManager_CharacterComplete;
            this.loadList.Add(characterHandle);

            if (string.IsNullOrEmpty(data.BGM) == false)
            {
                var bgmHandle = Static.SoundManager.Instance.LoadSound(data.BGM);
                bgmHandle.Completed += StageManager_BGMComplete;
                this.loadList.Add(bgmHandle);
            }
        }

        private void StageManager_WorldComplete(AsyncOperationHandle<SceneInstance> obj)
        {
            //currentWorldHandle = obj;
            foreach(var root in obj.Result.Scene.GetRootGameObjects())
            {
                if(root.GetComponentsInChildren<MapComponent.SpawnPoint>().Length > 0)
                {
                    this.world = root;
                    break;
                }
            }
        }

        private void InitStage()
        {
            if (this.boss != null)
                GameObject.Destroy(this.boss);
            if (this.world != null)
                GameObject.Destroy(this.world);
            if (this.character != null)
                GameObject.Destroy(this.character);

            Static.SoundManager.Instance.ClearSound();

            this.boss = null;
            this.character = null;
            this.world = null;
            this.bgmClip = null;

            this.loadList.Clear();
            if (this.currentWorldHandle.IsValid())
            {
                Addressables.UnloadSceneAsync(this.currentWorldHandle, true);
            }
        }

        private void StageManager_BossComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            this.boss = obj.Result;
            this.boss.gameObject.SetActive(false);
        }
        private void StageManager_CharacterComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
        {
            this.character = obj.Result;
            this.character.gameObject.SetActive(false);
        }
        private void StageManager_BGMComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AudioClip> obj)
        {
            this.bgmClip = obj.Result;
        }

        private void SetObjectPosition()
        {
            this.world.transform.position = Vector3.zero;
            var spawnPointList = this.world.GetComponentsInChildren<MapComponent.SpawnPoint>();
            foreach (var point in spawnPointList)
            {
                if (point.eSpawnType == MapComponent.SpawnPoint.SpawnType.Boss)
                {
                    NavMeshHit closestHit;
                    if (NavMesh.SamplePosition(point.transform.position, out closestHit, 500, 1))
                    {
                        this.boss.transform.position = closestHit.position;
                        //var navMeshAgent = this.boss.GetComponent<NavMeshAgent>();
                        //navMeshAgent.enabled = false;
                        //navMeshAgent.enabled = true;
                    }
                    else
                    {
                        Debug.LogError("NavMesh Create Fail");
                    }
                }
                else if (point.eSpawnType == MapComponent.SpawnPoint.SpawnType.Character)
                {
                    //var characterController = this.character.GetComponent<CharacterController>();
                    //characterController.enabled = false;
                    this.character.transform.position = point.transform.position;
                    //characterController.enabled = true;
                }
            }
            this.character.gameObject.SetActive(true);
            this.boss.gameObject.SetActive(true);
        }
    }
}