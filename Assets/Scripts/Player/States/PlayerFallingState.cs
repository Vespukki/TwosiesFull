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

            AirMove();
        }
    }
}