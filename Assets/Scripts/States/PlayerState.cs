using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.States.Player;
using Twosies.Player;

namespace Twosies.States
{
    public class PlayerState : BaseState
    {
        protected InputStateMachine inputSM;
        protected PlayerInput input;
        protected Rigidbody2D body;
        protected SpriteRenderer spriter;
        protected Animator animator;

        protected InputAction moveAction;
        protected InputAction jumpAction;
        protected InputAction interactAction;
        protected bool canTurn = true;

        protected bool canInteract = true;

        public PlayerState(InputStateMachine _sm) : base(_sm)
        {
            inputSM = _sm;
            input = inputSM.input;
            body = inputSM.body;
            spriter = inputSM.spriter;
            animator = inputSM.animator;

            moveAction = input.actions.FindAction("Move");
            jumpAction = input.actions.FindAction("Jump");
            interactAction = input.actions.FindAction("Interact");

        }

        

        

        public override void StateEnter()
        {
            base.StateEnter();
            InputStateMachine.OnInteract += InteractInput;
            InputStateMachine.OnJump += JumpInput;
        }

        public override void StateExit()
        {
            base.StateExit();

            InputStateMachine.OnInteract -= InteractInput;
            InputStateMachine.OnJump -= JumpInput;
        }


        protected virtual void InteractInput()
        {
                PlayerStateMachine newPlayer = GameManager.instance.SpawnPlayer(inputSM.transform.position);

                PlayerSoul.TransferSoul(inputSM, newPlayer);
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

        protected void Move(float input, float speed, float acceleration, float decceleration, float jerk,
            float targetTolerance, float overSpeedDecceleration) //moves x only
        {
            float targetSpeed = input * speed; //dir to move in at speed

            float speedDiff = targetSpeed - body.velocity.x; //diff between current and desired


            if (Mathf.Abs(targetSpeed) - targetTolerance <= Mathf.Abs(body.velocity.x) && Mathf.Abs(body.velocity.x) <= Mathf.Abs(targetSpeed) + targetTolerance
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

                if (canTurn && targetSpeed != 0)
                {
                    inputSM.facingRight = input < 0;
                }
            }
        }
    }
}