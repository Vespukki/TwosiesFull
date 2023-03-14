using System.Collections;
using System.Collections.Generic;
using Twosies.States.Player;
using UnityEngine;

namespace Twosies.Interactable.Possessable
{
    public class HeavyArmor : PossessableBase
    {
        protected override void Possess(PlayerStateMachine player)
        {
            base.Possess(player);

            Debug.Log("Possessed Heavy Armor");
        }
    }
}
