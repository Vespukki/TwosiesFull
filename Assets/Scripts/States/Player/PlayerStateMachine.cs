using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Twosies.Player;

namespace Twosies.States.Player
{
    public class PlayerStateMachine : InputStateMachine
    {
        protected override void Awake()
        {
            base.Awake();

            canInteract = true;
        }

        public override void OnSoulEnter(PlayerSoul newSoul)
        {
            base.OnSoulEnter(newSoul);
            ChangeState(new PlayerWalkingState(this));
        }

        public override void OnSoulExit()
        {
            base.OnSoulExit();
            Destroy(gameObject);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

