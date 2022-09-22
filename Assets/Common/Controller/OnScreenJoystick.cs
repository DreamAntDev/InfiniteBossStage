using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;
using System.Linq;

namespace Common.Controller
{
    public class OnScreenJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public RectTransform stick;
        public int range;

        private RectTransform rectTransform;
        private Camera canvasCamera;
        private void Awake()
        {
            // Common�ϰ� ������ InputActionAsset�� ��ũ�ؼ� ����ϵ��� �����ؾ� �Ѵ�.
            PlayerInputActions playerInputActions = new PlayerInputActions();
            InputActionMap inputActionMap = null;
            InputAction inputAction = null;

            foreach(var actionMap in playerInputActions.asset.actionMaps)
            {
                var actions = actionMap.actions;
                foreach(var action in actions)
                {
                    int findIdx = action.bindings.IndexOf(o => o.path.Equals(this.controlPath));
                    if(findIdx >= 0)
                    {
                        inputAction = action;
                        inputActionMap = actionMap;
                        break;
                    }
                }
                if(inputAction != null && inputActionMap != null)
                {
                    break;
                }
            }

            inputActionMap.Enable();
            inputAction.performed += SetPosition;
            inputAction.canceled += ResetPosition;

            //playerInputActions.Player.Enable();
            //playerInputActions.Player.Move.performed += SetPosition;
            //playerInputActions.Player.Move.canceled += ResetPosition;

            this.rectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            stick.anchoredPosition = input * range;
        }

        public void ResetPosition(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            stick.anchoredPosition = input * range;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(Vector2.zero);
            stick.anchoredPosition = Vector2.zero;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // eventData.position �� ���ϴ� ���� ��ǥ, rectTransform�� ��Ŀ�� ���ϴ����� ��� �Ǻ��� �߽����� ���߸� ���� ��ǥ��� ��� �����ϴ�
            // Common�ϰ� ��Ŀ�� �µ��� UI�� ��ǥ�� ��ũ����ǥ�� ���ߴ� �۾��� �ʿ��ϴ�
            // eventData�� �Ѿ���°� ��ũ�� ��ǥ, ��Ŀ�� ��ġ�� ���� this�� �߽� ��ġ�� �޶�����.
            if (canvasCamera == null)
            {
                var canvas = this.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                    {
                        canvasCamera = canvas.worldCamera;
                    }
                }
            }

            var dirVec = eventData.position;
            if (canvasCamera != null)
            {
                dirVec -= RectTransformUtility.WorldToScreenPoint(canvasCamera, this.transform.position);
            }
            else
            {
                dirVec -= this.rectTransform.anchoredPosition;
            }
            
            if (dirVec.magnitude > range)
            {
                dirVec = dirVec.normalized;
            }
            else
            {
                dirVec /= range;
            }

            SendValueToControl(dirVec);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var dirVec = eventData.position;
            if(canvasCamera == null)
            {
                var canvas = this.GetComponentInParent<Canvas>();
                if (canvas != null)
                {
                    if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                    {
                        canvasCamera = canvas.worldCamera;
                    }
                }
            }

            if (canvasCamera != null)
            {
                dirVec -= RectTransformUtility.WorldToScreenPoint(canvasCamera, this.transform.position);
            }
            else
            {
                dirVec -= this.rectTransform.anchoredPosition;
            }

            if (dirVec.magnitude > range)
            {
                dirVec = dirVec.normalized;
            }
            else
            {
                dirVec /= range;
            }

            SendValueToControl(dirVec);
        }


        [SerializeField, InputControl(layout = "Vector2")]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}