using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Twosies.States
{
    public class PlayerState : BaseState
    {
        protected PlayerStateMachine playerSM;
        protected PlayerInput input;
        public PlayerState(PlayerStateMachine _sm) : base(_sm)
        {
            playerSM = _sm;
            input = playerSM.GetComponent<PlayerInput>();
        }
    }
}