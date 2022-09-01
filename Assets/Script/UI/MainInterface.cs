using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.MainInterface
{
    public class MainInterface : MonoBehaviour
    {
        static MainInterface instance = null;
        public static MainInterface Instance
        {
            get { return MainInterface.instance; }
        }

        public Common.UI.SlicedFilledSlider hpSlider;
        public Common.UI.SlicedFilledSlider staminaSlider;
        public Common.UI.SlicedFilledSlider bossHpSlider;

        private void Awake()
        {
            MainInterface.instance = this;

            Common.Controller.PlayerInputActions playerInputActions = new Common.Controller.PlayerInputActions();
            playerInputActions.Enable();
            playerInputActions.Player.Menu.performed += OnMenuPerform;
        }

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Canvas>().worldCamera = Static.CameraManager.Instance.UICamera;
            this.transform.SetParent(Static.CameraManager.Instance.UICamera.transform);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMenuPerform(InputAction.CallbackContext context) // input관련 코드정리가 필요
        {
            bool UIBackButtonSuccess = UI.UILoader.PopBackButton();
            if (UIBackButtonSuccess == true)
                return;

            if (GameManager.Instance.state == GameManager.GameState.Stage)
            {
                if (UI.CommonPopup.CommonPopup.popup == null)
                {
                    // MenuUI
                    UI.CommonPopup.CommonPopup.CommonPopupContext popupContext = new UI.CommonPopup.CommonPopup.CommonPopupContext();
                    popupContext.confirm = () =>
                    {
                        GameManager.Instance.OnLobby();
                        UI.CommonPopup.CommonPopup.Close();
                    };
                    popupContext.needCloseButton = true;
                    UI.CommonPopup.CommonPopup.Open(popupContext);
                }
            }
        }

    }
}