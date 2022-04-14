using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingPage : MonoBehaviour
    {
        public Image image;
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.LobbyCamera;
            this.transform.SetParent(Static.CameraManager.Instance.LobbyCamera.transform);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}