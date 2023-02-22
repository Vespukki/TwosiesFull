using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace States
{

    public abstract class PlayerMovementState : PlayerState
    {
        protected InputAction moveAction;

        public PlayerMovementState(PlayerStateMachine _sm) : base(_sm)
        {
            moveAction = input.actions.FindAction("Move");
        }

        protected virtual void SetGravity()
        {
            body.gravityScale = (2 * playerSM.stats.JumpHeight) / (playerSM.stats.JumpTime * playerSM.stats.JumpTime);
        }

        public override void StateEnter()
        {
            base.StateEnter();
            SetGravity();
            PlayerStateMachine.OnJump += Jump;
        }

        public override void StateExit()
        {
            base.StateExit();

            PlayerStateMachine.OnJump -= Jump;
        }

        protected virtual void Jump()
        {
            
        }

        protected void Move(float input, float speed, float acceleration,float decceleration, float jerk, float targetTolerance) //moves x only
        {
            //if (Mathf.Abs(body.velocity.x) > playerSM.stats.Speed && Mathf.Sign(body.velocity.x) == Mathf.Sign(moveAction.ReadValue<float>())) return;


            float targetSpeed = input * speed; //dir to move in at speed

            float speedDiff = targetSpeed - body.velocity.x; //diff between current and desired


            if(Mathf.Abs(targetSpeed) - targetTolerance <= Mathf.Abs(body.velocity.x) && Mathf.Abs(body.velocity.x) <= Mathf.Abs(targetSpeed) + targetTolerance
                && Mathf.Abs(body.velocity.x) != Mathf.Abs(targetSpeed))
            {
                body.velocity = new Vector2(targetSpeed, body.velocity.y);
            }
            else
            {
                bool accel = Mathf.Abs(targetSpeed) > .01; //choose to accelerate or decelerate
                float accelRate = accel ? acceleration : decceleration;

                //applies acceleration to speedDiff, then sets to a power to set jerk. then reapplies direction
                float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, jerk) * Mathf.Sign(speedDiff);

                body.AddForce(movement * Vector2.right);
            }


        }
    }

}