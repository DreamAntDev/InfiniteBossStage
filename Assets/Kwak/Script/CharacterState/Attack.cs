using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Attack : IState
    {
        bool nextAttck = false;
        List<string> aniName = new List<string>
        {
            "Attack01",
            "Attack02"
        };
        int curAnimIdx = 0;
        string curPlayAniName = string.Empty;

        public override void Entry()
        {
            nextAttck = false;
            curAnimIdx = 0;
            PlayAnim();
        }

        public override void Exit()
        {
            
        }

        public override bool Update()
        {
            var curAnimatorStateInfo = PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0);
            if (curAnimatorStateInfo.IsName(this.curPlayAniName) == false)
            {
                // 트랜지션 중인 상태
                return true;
            }

            if (curAnimatorStateInfo.normalizedTime < 1.0f)
            {
                return true;
            }

            if (this.nextAttck == true)
            {
                this.curAnimIdx++;
                this.nextAttck = false;
                PlayAnim();
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

            var curAnimatorStateInfo = PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0);
            if (curAnimatorStateInfo.IsName(this.curPlayAniName) == false)
            {
                return false;
            }

            if (curAnimatorStateInfo.normalizedTime < 1.0f)
            {
                return false;
            }
            return true;
        }
        public void SetCombo()
        {
            if (PlayerController.instance.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
            {
                nextAttck = true;
            }
        }

        void PlayAnim()
        {
            if (this.curAnimIdx >= this.aniName.Count)
                this.curAnimIdx = 0;

            this.curPlayAniName = this.aniName[curAnimIdx];
            PlayerController.instance.animator.CrossFadeInFixedTime(this.curPlayAniName, 0.1f);
        }
    }
}