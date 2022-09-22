
using IBS.Resoruce;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Type = IBS.Resoruce.Type;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace UI.Lobby
{
    public class InventoryPage : LobbyPage
    {
        private const int RATING_NORMAL = 0;
        private const int RATING_EPIC = 1;
        private const int RATING_UNIQUE = 2;


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
        protected GameObject menu_Focus;

        [SerializeField]
        protected List<GameObject> menu_ActiveRelicPointList;

        [SerializeField]
        protected Transform subTransform;

        [SerializeField]
        protected List<RelicIcon> relicIcons;

        [SerializeField]
        protected GameObject menu_SelectButton;
        [SerializeField]
        protected GameObject menu_DeSelectButton;

        [SerializeField]
        protected GameObject newIcon;

        [SerializeField]
        protected GameObject selectIcon;

        private RelicIcon currentRelicIcon;
        private List<RelicIcon> relicIconList;
        private List<RelicIcon> selectRelicIconList = new List<RelicIcon>();
        private void Awake()
        {
            relicIconList = new List<RelicIcon>();
        }

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

                var relicFindData = relicIconList.Find(x => x.Equals(relic));
                if (relicFindData is null)
                {
                    GameObject obj = Instantiate(iconData.gameObject);
                    obj.transform.SetParent(relicParent);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localScale = Vector3.one;

                    RelicIcon icon = obj.GetComponent<RelicIcon>();
                    icon.OnItemClick += OnRelicIconClick;
                    icon.SetRelicIcon(relic);

                    relicIconList.Add(icon);

                    if (RelicManager.Instance.ActiveRelics.Exists(x => x.ID == relic.ID))
                    {
                        currentRelicIcon = icon;
                        SetSelectRelicPoint();
                    }
                }
                else
                {
                    relicFindData.UpdateItemCount();
                }

            }
        }

        public void OnRelicIconClick(RelicIcon relicicon)
        {
            menu_Focus.transform.SetParent(relicicon.gameObject.transform);
            menu_Focus.transform.localPosition = Vector3.zero;

            if (!menu_Focus.activeSelf)
            {
                menu_Focus.SetActive(true);
            }

            if(relicicon.point == null)
            {
                menu_SelectButton.SetActive(true);
                menu_DeSelectButton.SetActive(false);
            }
            else
            {
                menu_DeSelectButton.SetActive(true);
                menu_SelectButton.SetActive(false);
            }

            Relic relic = relicicon.Relic;
            //Selectd Relic
            menu_Rating.text = relic.Rating.ToString();
            menu_RelicName.text = relic.Name;
            menu_Info.text = relic.Context;
            //menu_Status.text = relic.

            currentRelicIcon = relicicon;
        }

        public void SelectRelic(bool isSelect)
        {
            if (isSelect)
            {
                SetSelectRelicPoint();
                RelicManager.Instance.SelectRelic(currentRelicIcon.Relic);
            }
            else
            {
                DeSelectRelicPoint(currentRelicIcon);
                RelicManager.Instance.DeSelectRelic(currentRelicIcon.Relic);
            }
        }

        private void SetSelectRelicPoint()
        {
            if(selectRelicIconList.Count == 3)
            {
                RelicIcon icon = selectRelicIconList.First();
                DeSelectRelicPoint(icon);
                selectRelicIconList.Remove(icon);
            }

            GameObject activePointObj = subTransform.Find("ActiveRelicTap")?.gameObject;

            activePointObj.transform.SetParent(currentRelicIcon.transform);
            activePointObj.transform.localPosition = Vector3.zero;
            currentRelicIcon.point = activePointObj;

            selectRelicIconList.Add(currentRelicIcon);
            
            menu_DeSelectButton.SetActive(true);
            menu_SelectButton.SetActive(false);
        }

        private void DeSelectRelicPoint(RelicIcon ri)
        {
            Debug.Log("DeSelect");
            var point = ri.point;
            point?.transform.SetParent(subTransform);

            menu_DeSelectButton.SetActive(false);
            menu_SelectButton.SetActive(true);
            selectRelicIconList.Remove(ri);
            ri.point = null;
        }

        public override string GetTitle()
        {
            return "Inventory";
        }
    }
}