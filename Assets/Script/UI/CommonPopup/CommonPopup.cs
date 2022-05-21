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

        public struct CommonPopupContext
        {
            public UnityEngine.Events.UnityAction confirm;
        }

        private void Start()
        {

        }

        public static void Close()
        {
            UI.UILoader.Unload("CommonPopup");
        }
        public static void Open(CommonPopupContext context)
        {
            UI.UILoader.Load("CommonPopup", () => LoadComplete(context));
        }
        private static void LoadComplete(CommonPopupContext context)
        {
            var popup = UI.UILoader.GetUI<UI.CommonPopup.CommonPopup>("CommonPopup");
            popup.confirmButton.onClick.AddListener(context.confirm);
        }
    }
}