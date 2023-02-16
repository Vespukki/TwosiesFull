using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States
{
    public class PlayerIdleState : PlayerState
    {
        private InputAction moveAction;

        public PlayerIdleState(PlayerStateMachine _sm) : base(_sm)
        {
            moveAction = input.actions.FindAction("Move");
        }

        public override void TryTransitions()
        {
            base.TryTransitions();

            if(moveAction.ReadValue<float>() != 0f)
            {
                playerSM.ChangeState(new PlayerWalkingState(playerSM));
            }
        }
    }
}