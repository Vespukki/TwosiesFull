using System.Collections;
using System.Collections.Generic;
using Twosies.States;
using UnityEngine;

namespace Twosies.Interactable.Possessable
{
    public abstract class PossessableBase : InteractableBase
    {
        protected InputStateMachine stateMachine;

        public override bool CanHighlight()
        {
            if(stateMachine.soul == null)
            {
                return true;
            }
            return false;
        }

        private void Awake()
        {
            stateMachine = GetComponent<InputStateMachine>();
        }

        public override void Interact(InputStateMachine player)
        {
            base.Interact(player);

            Possess(player);
        }

        protected virtual void Possess(InputStateMachine player)
        {

        }

    }
}
