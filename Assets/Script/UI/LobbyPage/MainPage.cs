using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace UI.Lobby
{
    [RequireComponent(typeof(DropRelic))]
    public class MainPage :LobbyPage
    {
        public Button InventoryButton;
        public Button StageButton;
        public Button SettingButton;
        public Button ShopButton;
        public TextMeshProUGUI Text_HP;
        public TextMeshProUGUI Text_Stamina;
        public TextMeshProUGUI Text_Attack;
        public TextMeshProUGUI Text_Move;
        public TextMeshProUGUI Text_Achievement_Stage;
        public TextMeshProUGUI Text_Achievement_Relics;
        public Slider Slider_Achievement_Stage;
        public Slider Slider_Achievement_Relics;


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
            SettingButton.onClick.AddListener(OnSettingPopup);
            ShopButton.onClick.AddListener(OnShop);
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

        private void OnEnable()
        {
            status = new Character.Status(statusData);
            PlayerStatus();
            Achevement();

        }

        private void OnShop()
        {
            var relics = RelicManager.Instance.Relics;
            var itemRating = DropRelic.GetChanceResult();
            Debug.Log("Relic Rating : " + itemRating);
            var relic = relics.Find(x => x.Rating == itemRating);
            if(relic is null)
            {
                Debug.Log($"{gameObject.name} Not Relic : {itemRating}");
                return;
            }
            else
            {
                RelicManager.Instance.AddRelic(relic);
                UI.UILoader.Load("RelicRewardPopup", () => UI.UILoader.GetUI<UI.RelicRewardPopup.RelicRewardPopup>("RelicRewardPopup").SetRelicData(relic));
            }
        }

        private void Achevement()
        {
            var achievements = new List<string>();
            achievements = Static.AchievementManager.get();
            for(int i = 0; i < achievements.Count; i++) {
                Debug.Log(achievements[i]);
                string[] infos = achievements[i].Split(':');
                switch (infos[0])
                {
                    case "0":
                        // 스테이지
                        Text_Achievement_Stage.text = infos[1] + "/" + infos[2];
                        Slider_Achievement_Stage.value = float.Parse(infos[1])/float.Parse(infos[2]);
                        break;
                    case "1":
                        // 렐릭
                        Text_Achievement_Relics.text = infos[1] + "/" + infos[2];
                        Slider_Achievement_Relics.value = float.Parse(infos[1])/float.Parse(infos[2]);
                        break;
                }
            }
        }

        private void PlayerStatus()
        {

            Text_HP.text = this.status.MaxHP.ToString() + ((this.status.CurrentHP > this.status.MaxHP) ?  " +"+(this.status.CurrentHP - this.status.MaxHP).ToString() : "");
            Text_Stamina.text = this.status.MaxStamina.ToString() + ((this.status.CurrentStamina > this.status.MaxStamina) ?  " +"+(this.status.CurrentStamina - this.status.MaxStamina).ToString() : "");
            Text_Attack.text = this.status.MaxAttack.ToString() + ((this.status.CurrentAttack > this.status.MaxAttack) ?  " +"+(this.status.CurrentAttack - this.status.MaxAttack).ToString() : "");
            Text_Move.text = this.status.MaxMove.ToString() + ((this.status.CurrentMove > this.status.MaxMove) ?  " +"+(this.status.CurrentMove - this.status.MaxMove).ToString() : "");

            List<Relic> activeRelics = RelicManager.Instance.ActiveRelics;
            Debug.Log("PlayerStatus ActiveRelic:" + activeRelics.Count);
            for(int i = 0; i < RELIC_CNT_LIMIT; i++)
            {
                var relicUI = GameObject.Find("Button_Relic_" + i.ToString());
                var image_Relic = relicUI.transform.Find("Icon_Sub").GetComponent<Image>();
                var Text_Relic_Name = relicUI.transform.Find("Text_Relic_Name").GetComponent<TextMeshProUGUI>();
                var Text_Relic_Rating = relicUI.transform.Find("Text_Relic_Rating").GetComponent<TextMeshProUGUI>();
                var Text_Relic_Context = relicUI.transform.Find("Text_Relic_Context").GetComponent<TextMeshProUGUI>();

                if ((activeRelics.Count - i) > 0) {
                    image_Relic.enabled = true;
                    image_Relic.sprite = activeRelics[i].Sprite;
                    Text_Relic_Name.text = activeRelics[i].Name;
                    Text_Relic_Rating.text = activeRelics[i].Rating.ToString();
                    Text_Relic_Context.text = activeRelics[i].Context;
                } else {
                    image_Relic.enabled = false;
                    Text_Relic_Name.text = "";
                    Text_Relic_Rating.text = "";
                    Text_Relic_Context.text = "";
                }
            }
        }
    }
}
