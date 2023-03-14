using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States.Player
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

        protected override void SetAnimation()
        {
            base.SetAnimation();

            playerSM.animator.CrossFade(FALL_ANIM, 0);
        }


        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            AirMove();
        }
    }
}