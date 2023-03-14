using System.Collections;
using System.Collections.Generic;
using Twosies.States;
using UnityEngine;

namespace Twosies.Interactable.Possessable
{
    public abstract class PossessableBase : InteractableBase
    {
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
