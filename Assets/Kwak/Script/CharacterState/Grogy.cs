using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public class Grogy : IState
    {
        public class Context
        {
            public float time;
            public Vector3 moveVector;
            public GrogyType grogyType;
            public float moveTime;
            public Context(GrogyType grogyType, float time, Vector3 moveVector, float moveTime)
            {
                this.grogyType = grogyType;
                this.time = time;
                this.moveVector = moveVector;
                this.moveTime = moveTime;
            }
        }

        [Flags]
        public enum GrogyType
        {
            // Upper와 Push만 같이 사용 가능 다른 케이스는 추가 구현필요
            None = 0,
            Upper = 1,
            Push = 2,
        }

        private Context context = null;
        private float accumTime = 0.0f;
        private string curAnimName = string.Empty;

        public override void Entry()
        {
            this.context = null;
            this.accumTime = 0.0f;
            curAnimName = string.Empty;
        }

        public override void Exit()
        {

        }

        public override bool Update()
        {
            if (this.context == null)
                return false;

            this.accumTime += Time.deltaTime;
            if (this.accumTime >= this.context.time)
                return false;

            if(this.accumTime >= this.context.moveTime && curAnimName != "Dizzy")
            {
                PlayerController.instance.animator.CrossFadeInFixedTime("Dizzy", 0.0f);
                curAnimName = "Dizzy";
            }

            if (this.accumTime < this.context.moveTime)
            {
                if (this.context.grogyType.HasFlag(GrogyType.Push) || this.context.grogyType.HasFlag(GrogyType.Upper))
                {
                    if (this.context.grogyType.HasFlag(GrogyType.Upper) == true)
                    {
                        PlayerController.instance.AppendMoveVectorPerFrame(Time.deltaTime * this.context.moveVector);
                    }
                    else
                    {
                        // 수식이 이상한 것 같은데.....
                        Vector3 lerpVec = Vector3.Lerp(this.context.moveVector,Vector3.zero, this.accumTime / this.context.moveTime);
                        PlayerController.instance.AppendMoveVectorPerFrame(Time.deltaTime * lerpVec);
                    }
                }
            }

            return true;
        }

        public override State GetStateType()
        {
            return State.Grogy;
        }

        public override bool CanExit(State nextState)
        {
            if (this.accumTime >= this.context.time)
                return true;

            return false;
        }

        public void SetContext(Context context)
        {
            this.context = context;
            if (this.context.grogyType.HasFlag(GrogyType.Upper) == true)
            {
                PlayerController.instance.defaultMoveVector = Vector3.zero;
                PlayerController.instance.animator.CrossFadeInFixedTime("Die", 0.1f);
                curAnimName = "Die";
            }
            else
            {
                PlayerController.instance.animator.CrossFadeInFixedTime("Hit", 0.1f);
                curAnimName = "Hit";
            }
        }

        public void OnGround()//날아서 땅에 착지
        {
            if (this.context.grogyType.HasFlag(GrogyType.Upper) == true)
            {
                this.context.moveVector = Vector3.zero;
                PlayerController.instance.defaultMoveVector = new Vector3(0, -0.1f, 0);
                PlayerController.instance.animator.CrossFadeInFixedTime("DieRecover", 0.0f);
                curAnimName = "DieRecover";
            }
        }
    }
}