using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
