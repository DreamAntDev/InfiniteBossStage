using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SettingPopup
{
    public class SettingPopup : UI.Popup
    {
        public Button enterButton;
        public Button closeButton;

        private void Start()
        {
            this.enterButton.onClick.AddListener(() => Enter());
            this.closeButton.onClick.AddListener(() => Close());
        }


        private void OnClick()
        {
            Debug.Log("OnClick SettingPopup");
        }
        private void Close()
        {
            UI.UILoader.Unload("SettingPopup");
        }
        private void Enter()
        {
            Close();
        }
    }
}