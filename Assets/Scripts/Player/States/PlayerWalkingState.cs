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

        public override void StateEnter()
        {
            base.StateEnter();
        }

        protected override void JumpInput()
        {
            base.JumpInput();

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

            Move(moveAction.ReadValue<float>(), playerSM.stats.Speed, playerSM.stats.Acceleration, playerSM.stats.Decceleration, playerSM.stats.Jerk,
                playerSM.stats.MovementTargetTolerance, playerSM.stats.OverSpeedDecceleration);
        }
    }
}
