using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Attack : IState
    {
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFade("Attack01", 0.05f);
        }

        public override void Exit()
        {
            PlayerController.instance.animator.StopPlayback();
        }

        public override bool Update()
        {
            if (PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                return true;
            }
            return false;
        }

        public override State GetStateType()
        {
            return State.Attack;
        }
    }
}