using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Move : IState
    {
        Vector3 moveDirection;

        public override void Entry()
        {
            PlayerController.instance.animator.CrossFade("Run", 0.05f);
        }

        public override void Exit()
        {

        }

        public override State GetStateType()
        {
            return State.Move;
        }

        public override bool Update()
        {
            if (moveDirection != Vector3.zero)
            {
                PlayerController.instance.characterController.Move(Time.deltaTime * moveDirection * 3.0f);
                return true;
            }
            return false;
        }
        public void SetMoveDirection(Vector3 moveDirection)
        {
            this.moveDirection = moveDirection;

            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            PlayerController.instance.transform.rotation = toRotation;
        }
    }
}