using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Attack : IState
    {
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFadeInFixedTime("Attack01", 0.1f);
        }

        public override void Exit()
        {
            
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

        public override bool CanExit(State nextState)
        {
            if (nextState == State.Avoid)
                return true;

            if(PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                return false;
            }
            return true;
        }
    }
}