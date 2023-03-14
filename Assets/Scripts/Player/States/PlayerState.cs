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
        protected SpriteRenderer spriter;
        protected Animator animator;


        public PlayerState(PlayerStateMachine _sm) : base(_sm)
        {
            playerSM = _sm;
            input = playerSM.input;
            body = playerSM.body;
            spriter = playerSM.spriter;
            animator = playerSM.animator;
        }
    }
}