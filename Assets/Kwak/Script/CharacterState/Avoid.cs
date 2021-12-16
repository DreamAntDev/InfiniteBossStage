using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Avoid : IState
    {
        Vector3 moveVector = Vector3.zero;
        public override void Entry()
        {
            PlayerController.instance.animator.CrossFadeInFixedTime("DiveRoll", 0.1f);
        }

        public override void Exit()
        {
            
        }

        public override bool Update()
        {
            if (moveVector == Vector3.zero)
                return false;

            if (PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                PlayerController.instance.characterController.Move(Time.deltaTime * moveVector * 5.0f);
                return true;
            }
            return false;
        }

        public override State GetStateType()
        {
            return State.Avoid;
        }

        public override bool CanExit(State nextState)
        {
            if (PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                return false;
            }
            return true;
        }

        public void SetMoveVector(Vector3 moveVector)
        {
            this.moveVector = moveVector;
            Quaternion toRotation = Quaternion.LookRotation(moveVector);
            PlayerController.instance.transform.rotation = toRotation;
        }
    }
}