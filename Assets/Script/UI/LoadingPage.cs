using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingPage : MonoBehaviour
    {
        public delegate bool WaitFunc();
        public class FadeData
        {
            public float fadeInTime = 0.3f;
            public float fadeOutTime = 0.3f;
            public WaitFunc waitFunc; // false°¡ µÇ¸é fadein
            public FadeData(WaitFunc waitFunc, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
            {
                this.waitFunc = waitFunc;
                this.fadeOutTime = fadeOutTime;
                this.fadeInTime = fadeInTime;
            }
        }
        public Image image;
        private bool isLoading = false;
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.LoadingCamera;
            this.transform.SetParent(Static.CameraManager.Instance.LoadingCamera.transform);
        }

        public void SetLoading(FadeData data)
        {
            isLoading = true;
            Static.CameraManager.Instance.LoadingCamera.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
            StartCoroutine(Loading_Co(data));
        }
        public IEnumerator Loading_Co(FadeData data)
        {
            //var color = this.image.color;
            //color.a = 0.0f;
            //this.image.color = color;
            this.image.canvasRenderer.SetAlpha(0);
            this.image.CrossFadeAlpha(1.0f, data.fadeOutTime, true);
            yield return new WaitForSeconds(data.fadeOutTime);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(() => (data.waitFunc() == true));
            isLoading = false;
            this.image.CrossFadeAlpha(0.0f, data.fadeInTime, true);
            yield return new WaitForSeconds(data.fadeInTime);
            this.gameObject.SetActive(false);
            Static.CameraManager.Instance.LoadingCamera.gameObject.SetActive(false);
        }

        public bool IsLoading()
        {
            return this.isLoading;
        }
    }
}