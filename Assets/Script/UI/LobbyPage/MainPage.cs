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
        public TextMeshProUGUI Text_HP;
        public TextMeshProUGUI Text_Stamina;
        public TextMeshProUGUI Text_Attack;
        public TextMeshProUGUI Text_Move;

        public TextMeshProUGUI Text_Relic; 

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
            Debug.Log("MainPage Player Status Display");
            Text_HP.text =  this.status.MaxHP.ToString();
            Text_Stamina.text = this.status.MaxStamina.ToString();
            Text_Attack.text = this.status.MaxAttack.ToString();
            Text_Move.text = this.status.MaxMove.ToString();
            
            List<Relic> activeRelics = RelicManager.Instance.ActivePlayerRelic();
            int i = 0;
            foreach(var relic in activeRelics)
            {
                Text_Relic = GameObject.Find("Text_Relic_" + i.ToString()).GetComponent<TextMeshProUGUI>(); 
                Text_Relic.text = relic.ToString();
                i++;
                // todo ���� ���� ���� 3�� const ���� ����
                if (i > 2) {
                    break;
                }
            }
        }
    }
}
