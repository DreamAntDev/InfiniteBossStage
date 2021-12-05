using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;


namespace Common.Controller
{
    public class OnScreenJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public RectTransform stick;
        public int range;

        private RectTransform rectTransform;
        private void Awake()
        {
            PlayerInputActions playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Move.performed += SetPosition;
            playerInputActions.Player.Move.canceled += ResetPosition;

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
            var dirVec = eventData.position - this.rectTransform.anchoredPosition;
            
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
            var dirVec = eventData.position - this.rectTransform.anchoredPosition;

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