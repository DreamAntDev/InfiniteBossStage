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

            popup.closeButton.gameObject.SetActive(context.needCloseButton);
            if (context.needCloseButton == true)
            {
                popup.closeButton.onClick.AddListener(Close);
                UI.UILoader.PushBackButton(popup.closeButton);
            }
        }
    }
}