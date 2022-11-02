using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.CommonPopup
{
    public class CommonPopup : UI.Popup
    {
        public Button confirmButton;
        public Button closeButton;
        public static CommonPopup popup { get; private set; } = null;
        public struct CommonPopupContext
        {
            public UnityEngine.Events.UnityAction confirm;
            public bool needCloseButton;
            public string Title;
            public string Desc;
        }

        public override void SetBackButton()
        {
            UI.UILoader.PushBackButton(closeButton);
        }

        private void Start()
        {

        }

        public static void Close()
        {
            popup = null;
            UI.UILoader.Unload("CommonPopup");
        }
        public static void Open(CommonPopupContext context)
        {
            UI.UILoader.Load("CommonPopup", () => LoadComplete(context));
        }
        private static void LoadComplete(CommonPopupContext context)
        {
            popup = UI.UILoader.GetUI<UI.CommonPopup.CommonPopup>("CommonPopup");
            popup.confirmButton.onClick.AddListener(context.confirm);
            GameObject.Find("Text_Title").GetComponent<TextMeshProUGUI>().text = context.Title;
            GameObject.Find("Text_Desc").GetComponent<TextMeshProUGUI>().text = context.Desc;
            popup.closeButton.gameObject.SetActive(context.needCloseButton);
            if (context.needCloseButton == true)
            {
                popup.closeButton.onClick.AddListener(Close);
            }
        }
    }
}