using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Lobby
{
    public class Lobby : MonoBehaviour
    {
        public Button BackButton;
        public TextMeshProUGUI TitleText;

        public List<LobbyPage> pages;

        private Dictionary<string, LobbyPage> cachedLobbyDictionary = new Dictionary<string, LobbyPage>();
        private LobbyPage currentPage = null;

        private TextMeshPro tempTMP;

        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.LobbyCamera;
            this.transform.SetParent(Static.CameraManager.Instance.LobbyCamera.transform);
            this.BackButton.onClick.AddListener(()=>LoadPage());

            LoadPage();
            setCharacterStatus();
        }

        public void LoadPage(string pageName = "MainPage")
        {
            LobbyPage newPage;
            if(cachedLobbyDictionary.ContainsKey(pageName) == true)
            {
                newPage = cachedLobbyDictionary[pageName];
            }
            else
            {
                var obj = pages.Find(o => o.gameObject.name.Equals(pageName));
                if(obj != null)
                {
                    newPage = Instantiate(obj);
                    cachedLobbyDictionary.Add(pageName, newPage);
                    newPage.transform.SetParent(this.transform);
                    newPage.transform.SetAsFirstSibling();
                    newPage.transform.localScale = Vector3.one;
                    newPage.SetParentLobby(this);
                }
                else
                {
                    Debug.LogError("Not Registed Page Name");
                    return;
                }
            }

            newPage.transform.localPosition = Vector3.zero;
            newPage.gameObject.SetActive(true);

            // ���������� ó��
            if (this.currentPage != null)
            {
                this.currentPage.gameObject.SetActive(false);
            }

            this.currentPage = newPage;

            this.TitleText.gameObject.SetActive(this.currentPage.IsVisibleTitle());
            this.TitleText.SetText(this.currentPage.GetTitle());

            this.BackButton.gameObject.SetActive(this.currentPage.IsVisibleBackButton());
        }

        public void setCharacterStatus()
        {
            // TODO 캐릭터 스테이터스 조회
        }
    }
}