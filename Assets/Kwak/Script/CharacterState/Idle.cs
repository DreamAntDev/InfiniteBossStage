using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Idle : IState
    {
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFadeInFixedTime("Idle", 0.1f);
        }

        public override void Exit()
        {

        }

        public override State GetStateType()
        {
            return State.Idle;
        }

        public override bool Update()
        {
            return true;
        }

        public override bool CanExit(State nextState)
        {
            return true;
        }
    }
}