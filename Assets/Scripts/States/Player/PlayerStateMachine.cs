using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerStateMachine : InputStateMachine
    {
        protected override void Start()
        {
            base.Start();
            ChangeState(new PlayerWalkingState(this));
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

