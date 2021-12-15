using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.State
{
    public abstract class IState
    {
        public abstract void Entry();
        public abstract bool Update();
        public abstract void Exit();

        public abstract State GetStateType();
    }
    public enum State
    {
        Idle,
        Move,
        Attack,
        Avoid,
    }
}