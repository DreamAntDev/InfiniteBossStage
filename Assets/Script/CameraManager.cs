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
            this.UICamera.gameObject.SetActive(false);
        }
    }
}