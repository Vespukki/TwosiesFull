using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States
{
    public class PlayerWalkingState : PlayerState
    {
        InputAction moveAction;

        public PlayerWalkingState(PlayerStateMachine _sm) : base(_sm)
        {
            moveAction = input.actions.FindAction("Move");
        }

        public override void TryTransitions()
        {
            base.TryTransitions();

            if (moveAction.ReadValue<float>() == 0)
            {
                playerSM.ChangeState(new PlayerIdleState(playerSM));
            }
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            Move(moveAction.ReadValue<float>());
        }

        protected void Move(float dir)
        {
            playerSM.transform.position += .04f * Vector3.right * dir;
        }
    }
}
