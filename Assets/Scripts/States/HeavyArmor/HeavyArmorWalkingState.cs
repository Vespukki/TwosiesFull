using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States.HeavyArmor
{
    public class HeavyArmorWalkingState : HeavyArmorMovementState
    {
        public HeavyArmorWalkingState(HeavyArmorStateMachine _sm) : base(_sm)
        {
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();

            Move(moveAction.ReadValue<float>(), armorSM.stats.Speed, armorSM.stats.Acceleration, armorSM.stats.Decceleration,
                armorSM.stats.Jerk, armorSM.stats.MovementTargetTolerance, armorSM.stats.OverSpeedDecceleration);
        }

    }
}
