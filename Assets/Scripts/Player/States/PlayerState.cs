using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace States
{
    public class PlayerState : BaseState
    {
        protected PlayerStateMachine playerSM;
        protected PlayerInput input;
        protected Rigidbody2D body;
        public PlayerState(PlayerStateMachine _sm) : base(_sm)
        {
            playerSM = _sm;
            input = playerSM.GetComponent<PlayerInput>();
            body = playerSM.GetComponent<Rigidbody2D>();
        }
    }
}