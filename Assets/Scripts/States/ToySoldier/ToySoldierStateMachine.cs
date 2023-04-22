using System.Collections;
using System.Collections.Generic;
using Twosies.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Twosies.States.ToySoldier
{
    public class ToySoldierStateMachine : InputStateMachine
    {
        public float charge;
        public float chargeRate;
        public float dischargeRate;
        public float maxCharge;

        [SerializeField] private RectTransform barHolder;
        [SerializeField] private RectTransform bar;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            bar.sizeDelta = new Vector2(charge / maxCharge * barHolder.sizeDelta.x, bar.sizeDelta.y);

            
        }

        public override void SoullessFixedUpdate()
        {
            base.SoullessFixedUpdate();

            if (charge > 0)
            {
                charge -= dischargeRate;
                body.velocity = Vector2.right * (facingRight ? 1 : -1) * stats.Speed;

                if (facingRight)
                {
                    if (rightWalled)
                    {
                        facingRight = !facingRight;
                    }
                }
                else
                {
                    if (leftWalled)
                    {
                        facingRight = !facingRight;
                    }
                }

                if(charge <= 0)
                {
                    body.velocity *= Vector2.up;
                }
            }
        }

        public override void OnSoulEnter(PlayerSoul newSoul)
        {
            base.OnSoulEnter(newSoul);
            ChangeState(new ToySoldierIdleState(this));
        }


        public override void OnSoulExit()
        {
            base.OnSoulExit();
        }
    }
}
