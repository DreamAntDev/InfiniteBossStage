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
        public Button BattleButton;
        public Button SettingButton;
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
            BattleButton.onClick.AddListener(OnStage);
            SettingButton.onClick.AddListener(OnSettingPopup);
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

        private void OnSettingPopup()
        {
            UI.UILoader.Load("SettingPopup");
        }

        private void OnStage()
        {
            // todo ���� ������ �ε��� ��ȸ�ؼ� �ݿ�
            GameManager.Instance.OnStage(1);
        }

        private void Start() {    
            status = new Character.Status(statusData);
            PlayerStatus();
        }

        private void Update() 
        {
            // todo Update�� �������� ����, ���� ���� �̺�Ʈ�� ���� ���Ž�Ű����
            status = new Character.Status(statusData);
            PlayerStatus(); 
        }
        
        private void PlayerStatus()
        {
            Text_HP.text =  this.status.MaxHP.ToString();
            Text_Stamina.text = this.status.MaxStamina.ToString();
            Text_Attack.text = this.status.MaxAttack.ToString();
            Text_Move.text = this.status.MaxMove.ToString();
            
            List<Relic> activeRelics = RelicManager.Instance.ActivePlayerRelic();
            for(int i = 0; i < activeRelics.Count; i++)
            {
                var relicUI = GameObject.Find("Button_Relic_" + i.ToString()).GetComponent<TextMeshProUGUI>();
                var image_Relic = relicUI.GetComponentInChildren<Image>();
                var text_Relic = relicUI.GetComponentInChildren<TextMeshProUGUI>();
                image_Relic.sprite = activeRelics[i].Sprite;
                text_Relic.text = activeRelics[i].ToString();
            }
        }
    }
}
