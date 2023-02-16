using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerStateMachine : StateMachine
    {
        protected override void Start()
        {
            base.Start();
            ChangeState(new PlayerIdleState(this));
        }

        protected override void Update()
        {
            base.Update();
            Debug.Log(currentState);
        }
    }
}

