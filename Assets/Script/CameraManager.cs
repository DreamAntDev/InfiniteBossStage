using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Static
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; } = null;

        public Camera MainCamera;
        public Camera UICamera;        
        public Camera LobbyCamera;
        public Camera LoadingCamera;

        private void Awake()
        {
            if(CameraManager.Instance != null)
            {
                Debug.LogError(string.Format("CameraManager({0}) is Already Exist! Destroy Before Object!", CameraManager.Instance.gameObject.name));
                Destroy(CameraManager.Instance);
            }
            CameraManager.Instance = this;

            this.MainCamera = Camera.main;
            var targetTraceCamera = this.MainCamera.gameObject.AddComponent<Component.TargetTraceCamera>();
            targetTraceCamera.offset = new Vector3(0, 20, -20);

            this.UICamera = Instantiate(UICamera,this.transform);
            this.LobbyCamera = Instantiate(LobbyCamera, this.transform);
            this.LoadingCamera = Instantiate(LoadingCamera, this.transform);
            this.LoadingCamera.gameObject.SetActive(false);
            OnLobby();
        }

        public void OnLobby()
        {
            Static.CameraManager.Instance.LobbyCamera.gameObject.SetActive(true);
            //Static.CameraManager.Instance.MainCamera.gameObject.SetActive(false); // MainCam 없으면 Sound 안들림
            Static.CameraManager.Instance.UICamera.gameObject.SetActive(false);
        }

        public void OnStage()
        {
            Static.CameraManager.Instance.LobbyCamera.gameObject.SetActive(false);
            //Static.CameraManager.Instance.MainCamera.gameObject.SetActive(true);
            Static.CameraManager.Instance.UICamera.gameObject.SetActive(true);
        }
    }
}