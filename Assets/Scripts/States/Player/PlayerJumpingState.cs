using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.Physics;

namespace Twosies.States.Player
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

        protected override void SetAnimation()
        {
            base.SetAnimation();

            playerSM.animator.CrossFade(JUMP_ANIM, 0);
        }

        private void Jump()
        {
            Vector2 additionalJumpVelocity = Vector2.zero;

            foreach(var modifier in playerSM.jumpModifiers)
            {
                additionalJumpVelocity += playerSM.GetComponentInParent<PlayerAttacher>().JumpModifierVelocity;
            }

            body.AddForce(((2 * playerSM.stats.JumpHeight / playerSM.stats.JumpTime) * Vector2.up) + additionalJumpVelocity, ForceMode2D.Impulse);

            playerSM.transform.parent = null;
        }
    }
}
