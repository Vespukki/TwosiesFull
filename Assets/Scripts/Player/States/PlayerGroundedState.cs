using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class PlayerGroundedState : PlayerMovementState
    {
        public PlayerGroundedState(PlayerStateMachine _sm) : base(_sm)
        {

        }

        public override void StateEnter()
        {
            base.StateEnter();

            if(!(playerSM.previousState is PlayerGroundedState))
            {
                //do ground reset here when that exists
            }
        }
    }
}