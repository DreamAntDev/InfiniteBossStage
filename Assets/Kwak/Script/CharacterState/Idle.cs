using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Idle : IState
    {
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFade("Idle", 0.05f);
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
    }
}