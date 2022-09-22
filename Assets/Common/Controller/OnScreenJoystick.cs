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
            // Common하게 쓰려면 InputActionAsset을 링크해서 사용하도록 수정해야 한다.
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
            // eventData.position 은 좌하단 기준 좌표, rectTransform도 앵커를 좌하단으로 잡고 피봇을 중심으로 맞추면 동일 좌표계로 사용 가능하다
            // Common하게 앵커에 맞도록 UI상 좌표와 스크린좌표를 맞추는 작업이 필요하다
            // eventData로 넘어오는건 스크린 좌표, 앵커의 위치에 따라 this의 중심 위치가 달라진다.
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