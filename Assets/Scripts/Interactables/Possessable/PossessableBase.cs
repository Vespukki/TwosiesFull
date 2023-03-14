using System.Collections;
using System.Collections.Generic;
using Twosies.States.Player;
using UnityEngine;

namespace Twosies.Interactable.Possessable
{
    public abstract class PossessableBase : InteractableBase
    {
        public override void Interact(PlayerStateMachine player)
        {
            base.Interact(player);

            Possess(player);
        }

        protected virtual void Possess(PlayerStateMachine player)
        {

        }

    }
}
