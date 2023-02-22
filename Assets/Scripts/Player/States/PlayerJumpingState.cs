using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class PlayerJumpingState : PlayerAirState
    {
        public PlayerJumpingState(PlayerStateMachine _sm) : base(_sm)
        {
        }

        public override void TryTransitions()
        {
            base.TryTransitions();
            if(playerSM.grounded && body.velocity.y <= 0)
            {
                playerSM.ChangeState(new PlayerWalkingState(playerSM));
            }
            else if(body.velocity.y < 0)
            {
                playerSM.ChangeState(new PlayerFallingState(playerSM));
            }
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            Move(moveAction.ReadValue<float>(), playerSM.stats.AirSpeed, playerSM.stats.AirAcceleration,
                playerSM.stats.AirDecceleration, playerSM.stats.Jerk, playerSM.stats.MovementTargetTolerance);
        }

        public override void StateEnter()
        {
            base.StateEnter();

            body.AddForce((2 * playerSM.stats.JumpHeight / playerSM.stats.JumpTime) * Vector2.up, ForceMode2D.Impulse);
        }
    }
}
