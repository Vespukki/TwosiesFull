using System.Collections;
using System.Collections.Generic;
using Twosies.States;
using UnityEngine;

namespace Twosies.Interactable.Possessable
{
    public class HeavyArmor : PossessableBase
    {
        protected override void Possess(InputStateMachine player)
        {
            base.Possess(player);

            Debug.Log("Possessed Heavy Armor");
        }
    }
}
