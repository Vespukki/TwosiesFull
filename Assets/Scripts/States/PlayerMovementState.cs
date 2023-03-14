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

        protected PlayerStateMachine playerSM;

        protected PlayerMovementState(PlayerStateMachine _sm) : base(_sm)
        {
            playerSM = _sm;
        }
    }

}