using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States.HeavyArmor
{
    public class HeavyArmorStateMachine : InputStateMachine
    {
        protected override void Start()
        {
            base.Start();

            ChangeState(new HeavyArmorWalkingState(this));
        }
    }
}
