using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class StateMachine : MonoBehaviour
    {
        public BaseState currentState;
        public BaseState previousState;

       public void ChangeState(BaseState newState)
       {
            currentState?.StateExit();
            previousState = currentState;
            currentState = newState;
            currentState?.StateEnter();
       }

        protected virtual void Start() { }
        protected virtual void Awake() { }

        protected virtual void FixedUpdate()
        {
            currentState?.StateFixedUpdate();
            currentState?.TryTransitions();
        }

        protected virtual void Update()
        {
            currentState?.StateUpdate();
        }
    }
}

