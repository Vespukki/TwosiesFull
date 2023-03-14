using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States.Player
{
    public abstract class PlayerMovementState : PlayerState
    {
        #region animation hashes
        protected static readonly int IDLE_ANIM = Animator.StringToHash("Idle");
        protected static readonly int WALK_ANIM = Animator.StringToHash("Walk");
        protected static readonly int FALL_ANIM = Animator.StringToHash("Fall");
        protected static readonly int JUMP_ANIM = Animator.StringToHash("Jump");
        #endregion

        protected InputAction moveAction;
        protected bool canTurn = true;

        protected bool canInteract = true;

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
            PlayerStateMachine.OnJump += JumpInput;
            PlayerStateMachine.OnInteract += InteractInput;
        }

        public override void StateExit()
        {
            base.StateExit();

            PlayerStateMachine.OnJump -= JumpInput;
            PlayerStateMachine.OnInteract -= InteractInput;
        }

        protected virtual void InteractInput()
        {
            if(canInteract && playerSM.targetedInteractable != null)
            {
                playerSM.targetedInteractable.Interact(playerSM);
            }
        }

       

        protected virtual void JumpInput()
        {
            
        }

        protected virtual void SetAnimation()
        {

        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            SetAnimation();
        }

        protected void Move(float input, float speed, float acceleration,float decceleration, float jerk, 
            float targetTolerance, float overSpeedDecceleration) //moves x only
        {
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

                if (Mathf.Abs(body.velocity.x) > speed && Mathf.Sign(body.velocity.x) == Mathf.Sign(input))
                {
                    accelRate = overSpeedDecceleration;
                }

                //applies acceleration to speedDiff, then sets to a power to set jerk. then reapplies direction
                float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, jerk) * Mathf.Sign(speedDiff);

                body.AddForce(movement * Vector2.right);

                if(canTurn && targetSpeed != 0)
                {
                    playerSM.facingRight = input < 0;
                }
            }
        }
    }

}