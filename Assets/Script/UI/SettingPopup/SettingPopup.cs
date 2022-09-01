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

        [System.Serializable]
        public class BackgroundSound
        {
            public Slider slider;
        }
        public BackgroundSound backgroundSound;

        public override void SetBackButton()
        {
            UI.UILoader.PushBackButton(closeButton);
        }

        private void Start()
        {
            this.enterButton.onClick.AddListener(() => Enter());
            this.closeButton.onClick.AddListener(() => Close());

            backgroundSound.slider.value = Static.SoundManager.Instance.backgroundSoundValue;
            backgroundSound.slider.onValueChanged.AddListener((float val) => ChangeBackgroundSoundValue(val));
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

        private void ChangeBackgroundSoundValue(float value)
        {
            Static.SoundManager.Instance.SetSoundValue(value);
        }
    }
}