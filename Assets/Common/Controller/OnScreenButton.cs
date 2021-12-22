using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

namespace Common.Controller
{
    //[RequireComponent(typeof(Button))]
    public class OnScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        Button button;
        private void Awake()
        {
            // Common�ϰ� ������ InputActionAsset�� ��ũ�ؼ� ����ϵ��� �����ؾ� �Ѵ�.
            // Common������ ���� �ִ� InputAction�� ����ϴ� ��� ������
            PlayerInputActions playerInputActions = new PlayerInputActions();
            InputActionMap inputActionMap = null;
            InputAction inputAction = null;

            foreach (var actionMap in playerInputActions.asset.actionMaps)
            {
                var actions = actionMap.actions;
                foreach (var action in actions)
                {
                    int findIdx = action.bindings.IndexOf(o => o.path.Equals(this.controlPath));
                    if (findIdx >= 0)
                    {
                        inputAction = action;
                        inputActionMap = actionMap;
                        break;
                    }
                }
                if (inputAction != null && inputActionMap != null)
                {
                    break;
                }
            }

            inputActionMap.Enable();
            inputAction.performed += OnDown;
            inputAction.canceled += OnUp;

            this.button = GetComponent<Button>();
        }

        public void OnDown(InputAction.CallbackContext context)
        {
            if(this.button != null)
            {
                // Transition�� �ٸ� ���� ���Ŀ� ó��
                if(this.button.transition == Selectable.Transition.ColorTint)
                {
                    this.button.targetGraphic.CrossFadeColor(this.button.colors.pressedColor, this.button.colors.fadeDuration, true, true);
                }
            }
        }

        public void OnUp(InputAction.CallbackContext context)
        {
            if (this.button != null)
            {
                if (this.button.transition == Selectable.Transition.ColorTint)
                {
                    this.button.targetGraphic.CrossFadeColor(this.button.colors.normalColor, this.button.colors.fadeDuration, true, true);
                }
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1.0f);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
        }

        [SerializeField, InputControl(layout = "Button")]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }

}