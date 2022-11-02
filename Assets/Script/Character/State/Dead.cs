using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Dead : IState
    {
        bool isAnimPlay = false;
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFadeInFixedTime("Die", 0.1f);
            isAnimPlay = true;
        }

        public override void Exit()
        {

        }

        public override State GetStateType()
        {
            return State.Dead;
        }

        public override bool Update()
        {
            var curAnimatorStateInfo = PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0);
            if (isAnimPlay == true && curAnimatorStateInfo.IsName("Die") == true && curAnimatorStateInfo.normalizedTime > 1.0f)
            {
                isAnimPlay = false;
                UI.CommonPopup.CommonPopup.CommonPopupContext popupContext = new UI.CommonPopup.CommonPopup.CommonPopupContext();
                popupContext.confirm = () =>
                {
                    GameManager.Instance.OnLobby();
                    UI.CommonPopup.CommonPopup.Close();
                };
                popupContext.needCloseButton = false;
                popupContext.Title = "You Die";
                popupContext.Desc = "Come back stronger than you are now";

                UI.CommonPopup.CommonPopup.Open(popupContext);
            }
            return true;
        }

        public override bool CanExit(State nextState)
        {
            return false;
        }
    }
}