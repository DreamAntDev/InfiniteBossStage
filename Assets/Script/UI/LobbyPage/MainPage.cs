using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI.Lobby
{
    public class MainPage :LobbyPage
    {
        public Button InventoryButton;
        public Button StageButton;

        public override string GetTitle()
        {
            return "Main";
        }

        public override bool IsVisibleTitle()
        {
            return false;
        }
        public override bool IsVisibleBackButton()
        {
            return false;
        }

        private void Awake()
        {
            InventoryButton.onClick.AddListener(OnInventory);
            StageButton.onClick.AddListener(OnStage);
        }

        private void OnInventory()
        {
            this.parent.LoadPage("InventoryPage");
        }
        private void OnStage()
        {
            SceneManager.LoadScene("NammuiScene");
        }
    }
}
