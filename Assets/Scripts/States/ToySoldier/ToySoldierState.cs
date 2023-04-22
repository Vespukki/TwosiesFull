using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States;

namespace Twosies.States.ToySoldier
{
    public class ToySoldierState : PlayerState
    {
        protected ToySoldierStateMachine soldier;

        public ToySoldierState(ToySoldierStateMachine _sm) : base(_sm)
        {
            soldier = _sm;
        }

    }
}
