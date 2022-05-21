
using IBS.Resoruce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Type = IBS.Resoruce.Type;
using TMPro;

namespace UI.Lobby
{
    public class InventoryPage : LobbyPage
    {
        private const int RATING_NORMAL = 0;
        private const int RATING_EPIC = 0;
        private const int RATING_UNIQUE = 0;


        [SerializeField]
        protected Transform relicParent;

        [SerializeField]
        protected TextMeshProUGUI menu_Rating;
        [SerializeField]
        protected TextMeshProUGUI menu_RelicName;
        [SerializeField]
        protected TextMeshProUGUI menu_Info;
        [SerializeField]
        protected TextMeshProUGUI menu_Status;

        [SerializeField]
        protected List<RelicIcon> relicIcons;

        [SerializeField]
        protected GameObject newIcon;

        [SerializeField]
        protected GameObject selectIcon;

        private Relic currentRelic;
        private void Start()
        {
            InitUI();
        }

        private void InitUI()
        {
            var relics = RelicManager.Instance.Relics;
            Debug.Log($"### InitUI Relic List {relics.Count}");
            foreach(var relic in relics)
            {
                RelicIcon iconData = null;
                switch (relic.Rating)
                {
                    case Type.RelicRating.Normal:
                        iconData = relicIcons[RATING_NORMAL];
                        break;
                    case Type.RelicRating.Epic:
                        iconData = relicIcons[RATING_EPIC];
                        break;
                    case Type.RelicRating.Unique:
                        iconData = relicIcons[RATING_UNIQUE];
                        break;
                }
                GameObject obj = Instantiate(iconData.gameObject);

                obj.transform.SetParent(relicParent);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;


                RelicIcon icon = obj.GetComponent<RelicIcon>();
                icon.OnItemClick += OnRelicIconClick;
                icon.SetRelicIcon(relic);
            }
        }

        public void OnRelicIconClick(Relic relic)
        {
            //Selectd Relic
            menu_Rating.text = relic.Rating.ToString();
            menu_RelicName.text = relic.Name;
            menu_Info.text = relic.Context;
            //menu_Status.text = relic.

            currentRelic = relic;
        }

        public void SelectRelic()
        {
            RelicManager.Instance.SelectRelic(currentRelic);
        }

        public override string GetTitle()
        {
            return "Inventory";
        }
    }
}