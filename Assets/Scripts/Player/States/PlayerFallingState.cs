using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class PlayerFallingState : PlayerAirState
    {
        public PlayerFallingState(PlayerStateMachine _sm) : base(_sm)
        {
        }

        public override void TryTransitions()
        {
            base.TryTransitions();

            if(playerSM.grounded)
            {
                playerSM.ChangeState(new PlayerWalkingState(playerSM));
            }
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            Move(moveAction.ReadValue<float>(), playerSM.stats.AirSpeed, playerSM.stats.AirAcceleration, playerSM.stats.AirDecceleration, playerSM.stats.Jerk, playerSM.stats.MovementTargetTolerance);
        }
    }
}