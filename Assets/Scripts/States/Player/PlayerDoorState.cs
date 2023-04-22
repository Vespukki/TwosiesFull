using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States.Player
{
    public class PlayerDoorState : PlayerState
    {
        public PlayerDoorState(InputStateMachine _sm) : base(_sm)
        {
        }

        public override void StateEnter()
        {
            base.StateEnter();
            body.velocity *= Vector2.up;
        }
    }
}
