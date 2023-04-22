using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.States.ToySoldier
{
    public class ToySoldierIdleState : ToySoldierState
    {
        public ToySoldierIdleState(ToySoldierStateMachine _sm) : base(_sm)
        {
        }

        public override void StateEnter()
        {
            base.StateEnter();

            body.velocity *= Vector2.up;
        }

        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            
            if(jumpAction.ReadValue<float>() > 0)
            {
                if(soldier.charge < soldier.maxCharge)
                soldier.charge += soldier.chargeRate;
                body.velocity *= Vector2.up;
            }
            /*else if(soldier.charge > 0)
            {
                soldier.charge -= soldier.dischargeRate;
                body.velocity = Vector2.right * (soldier.facingRight ? 1 : -1) * soldier.stats.Speed;
            }
            else
            {
                body.velocity *= Vector2.up;
            }*/
        }


        public override void TryTransitions()
        {
            /*Debug.Log("try transition");
            base.TryTransitions();
            if (soldier.charge > 0 && soldier.soul == null)
            {
                Debug.Log("transition to mov");
                soldier.ChangeState(new ToySoldierWalkingState(soldier));
            }
*/
        }
    }
}
