using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Twosies.States.Player
{
    public class PlayerAirState : PlayerMovementState
    {
        public PlayerAirState(PlayerStateMachine _sm) : base(_sm)
        {
        }

        protected void AirMove()
        {
            Move(moveAction.ReadValue<float>(), playerSM.stats.AirSpeedMultiplier * playerSM.stats.Speed,
                   playerSM.stats.AirAccelerationMultiplier * playerSM.stats.Acceleration, playerSM.stats.AirDeccelerationMultiplier * playerSM.stats.Decceleration,
                   playerSM.stats.Jerk, playerSM.stats.MovementTargetTolerance, playerSM.stats.AirOverSpeedDeccelerationMultiplier * playerSM.stats.OverSpeedDecceleration);
        }
    }
}
