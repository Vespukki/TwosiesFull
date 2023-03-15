using System.Collections;
using System.Collections.Generic;
using Twosies.States;
using UnityEngine;
using Twosies.Player;

namespace Twosies.Interactable.Possessable
{
    public class HeavyArmor : PossessableBase
    {

        protected override void Possess(InputStateMachine player)
        {
            base.Possess(player);

            PlayerSoul.TransferSoul(player, stateMachine);
            Debug.Log("Possessed Heavy Armor");
        }
    }
}
