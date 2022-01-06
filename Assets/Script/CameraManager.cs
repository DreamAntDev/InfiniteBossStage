using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Static
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; } = null;

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

            this.UICamera = Instantiate(UICamera);
            this.LobbyCamera = Instantiate(LobbyCamera);
        }
    }
}