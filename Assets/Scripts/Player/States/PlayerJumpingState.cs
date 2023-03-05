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
            else if(body.velocity.y <= 0)
            {
                playerSM.ChangeState(new PlayerFallingState(playerSM));
            }
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            AirMove();
        }

        public override void StateEnter()
        {
            base.StateEnter();

            Jump();
        }

        private void Jump()
        {
            Vector2 additionalJumpVelocity;

            try
            {
                additionalJumpVelocity = playerSM.GetComponentInParent<PlayerAttacher>().velocity;
            }
            catch //means player not on an attacher
            {
                additionalJumpVelocity = Vector2.zero;
            }

            body.AddForce(((2 * playerSM.stats.JumpHeight / playerSM.stats.JumpTime) * Vector2.up) + additionalJumpVelocity, ForceMode2D.Impulse);

            playerSM.transform.parent = null;
        }
    }
}
