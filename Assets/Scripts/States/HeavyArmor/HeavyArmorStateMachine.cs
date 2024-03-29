using System.Collections;
using System.Collections.Generic;
using Twosies.Player;
using UnityEngine;

namespace Twosies.States.HeavyArmor
{
    public class HeavyArmorStateMachine : InputStateMachine
    {
        public override void OnSoulEnter(PlayerSoul newSoul)
        {
            base.OnSoulEnter(newSoul);
            ChangeState(new HeavyArmorWalkingState(this));
        }
    }
}
