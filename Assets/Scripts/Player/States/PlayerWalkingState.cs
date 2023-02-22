using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace States
{
    public class PlayerWalkingState : PlayerGroundedState
    {
        public PlayerWalkingState(PlayerStateMachine _sm) : base(_sm)
        {
        }

        protected override void Jump()
        {
            base.Jump();

            if(playerSM.grounded)
            {
                playerSM.ChangeState(new PlayerJumpingState(playerSM));
            }
        }

        public override void TryTransitions()
        {
            base.TryTransitions();

            if (moveAction.ReadValue<float>() == 0)
            {
            }
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            if (!(Mathf.Abs(body.velocity.x) > playerSM.stats.Speed && Mathf.Sign(body.velocity.x) == Mathf.Sign(moveAction.ReadValue<float>())))
            {
                Move(moveAction.ReadValue<float>(), playerSM.stats.Speed, playerSM.stats.Acceleration, playerSM.stats.Decceleration, playerSM.stats.Jerk,
                    playerSM.stats.MovementTargetTolerance);
            }
        }
    }
}
