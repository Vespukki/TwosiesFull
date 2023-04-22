using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.Player;
using Twosies;
using Twosies.States.Player;

namespace Twosies.States.HeavyArmor
{
    public abstract class HeavyArmorMovementState : PlayerState
    {
        protected HeavyArmorStateMachine armorSM;
        public HeavyArmorMovementState(HeavyArmorStateMachine _sm) : base(_sm)
        {
            armorSM = _sm;
        }
    }
}
