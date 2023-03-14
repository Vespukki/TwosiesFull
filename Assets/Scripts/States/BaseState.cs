using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States
{
    public abstract class BaseState
    {
        protected StateMachine stateMachine;
        public BaseState(StateMachine _sm)
        {
            stateMachine = _sm;
        }

        public virtual void StateEnter() { }
        public virtual void StateUpdate() { }
        public virtual void StateFixedUpdate() { }
        public virtual void StateExit() { }
        public virtual void TryTransitions() { }
    }
}
