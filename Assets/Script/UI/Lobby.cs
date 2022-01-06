using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class Lobby : MonoBehaviour
    {
        public List<LobbyPage> pages;
        private Dictionary<string, LobbyPage> cachedLobbyDictionary = new Dictionary<string, LobbyPage>();
        private LobbyPage currentPage = null;
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.LobbyCamera;
            LoadPage();
        }

        public void LoadPage(string pageName = "MainPage")
        {
            Transform pageTransform;
            if(cachedLobbyDictionary.ContainsKey(pageName) == true)
            {
                pageTransform = cachedLobbyDictionary[pageName].transform;
                //페이지 노출
            }
            else
            {
                var obj = pages.Find(o => o.gameObject.name.Equals(pageName));
                if(obj != null)
                {
                    var createdObj = Instantiate(obj);
                    cachedLobbyDictionary.Add(pageName, createdObj);
                    pageTransform = createdObj.transform;
                    pageTransform.SetParent(this.transform);
                    pageTransform.localScale = Vector3.one;
                    createdObj.SetParentLobby(this);
                }
                else
                {
                    Debug.LogError("Not Registed Page Name");
                    return;
                }
            }
            if (this.currentPage != null)
            {
                this.currentPage.gameObject.SetActive(false);
            }
            pageTransform.localPosition = Vector3.zero;
            pageTransform.gameObject.SetActive(true);
        }
    }
}