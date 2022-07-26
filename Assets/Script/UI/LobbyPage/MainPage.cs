using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI.Lobby
{
    public class MainPage :LobbyPage
    {
        public Button InventoryButton;
        public Button StageButton;
        public TextMeshProUGUI Text_HP;
        public TextMeshProUGUI Text_Stamina;
        public TextMeshProUGUI Text_Attack;
        public TextMeshProUGUI Text_Move;

        public Data.Character.CharacterStatus statusData;
        Character.Status status;

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
            StageButton.onClick.AddListener(OnStageEnterPopup);
        }

        private void OnInventory()
        {
            this.parent.LoadPage("InventoryPage");
        }
        private void OnStageEnterPopup()
        {
            UI.UILoader.Load("StageEnterPopup");
            //Debug.Log("Stage");
            //Static.StageManager.Instance.LoadStage(GameManager.Instance.StageIndex);
        }

        private void Start() {    
            PlayerStatus();
        }
        
        private void PlayerStatus()
        {
            Text_HP.text = statusData.MaxHP.ToString();
            Text_Stamina.text = statusData.MaxStamina.ToString();
            Text_Attack.text = statusData.MaxAttack.ToString();
            Text_Move.text = statusData.MaxMove.ToString();
        }
    }
}
