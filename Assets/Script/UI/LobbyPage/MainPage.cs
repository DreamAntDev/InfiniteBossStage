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

        private const int RELIC_CNT_LIMIT = 3;

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
            // todo 가장 마지막 인덱스 조회해서 반영
            GameManager.Instance.OnStage(1);
        }

        private void OnEnable()
        {
            status = new Character.Status(statusData);
            PlayerStatus();
        }

        private void PlayerStatus()
        {

            Text_HP.text = this.status.MaxHP.ToString() + ((this.status.CurrentHP > this.status.MaxHP) ?  " +"+(this.status.CurrentHP - this.status.MaxHP).ToString() : "");
            Text_Stamina.text = this.status.MaxStamina.ToString() + ((this.status.CurrentStamina > this.status.MaxStamina) ?  " +"+(this.status.CurrentStamina - this.status.MaxStamina).ToString() : "");
            Text_Attack.text = this.status.MaxAttack.ToString() + ((this.status.CurrentAttack > this.status.MaxAttack) ?  " +"+(this.status.CurrentAttack - this.status.MaxAttack).ToString() : "");
            Text_Move.text = this.status.MaxMove.ToString() + ((this.status.CurrentMove > this.status.MaxMove) ?  " +"+(this.status.CurrentMove - this.status.MaxMove).ToString() : "");

            List<Relic> activeRelics = RelicManager.Instance.ActivePlayerRelic();
            Debug.Log("PlayerStatus ActiveRelic:" + activeRelics.Count);
            // var image_Relic_default = relicUI.transform.Find("relic_icon_default").GetComponent<Image>();
            for(int i = 0; i < RELIC_CNT_LIMIT; i++)
            {
                var relicUI = GameObject.Find("Button_Relic_" + i.ToString());
                var image_Relic = relicUI.transform.Find("Icon_Sub").GetComponent<Image>();
                var text_Relic = relicUI.GetComponentInChildren<TextMeshProUGUI>();
                if ((activeRelics.Count - i) > 0) {
                    image_Relic.enabled = true;
                    image_Relic.sprite = activeRelics[i].Sprite;
                    text_Relic.text = activeRelics[i].ToString();
                } else {
                    image_Relic.enabled = false;
                    text_Relic.text = "";
                }
            }
        }
    }
}
