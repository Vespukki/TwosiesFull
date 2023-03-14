using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;

namespace Twosies.Player.Items
{
public abstract class ItemBase
{
    public ItemBase(PlayerStateMachine player)
    {
    }

    public string itemName;

    public abstract void Use(PlayerStateMachine player);

    public abstract void UnUse(PlayerStateMachine player);
}
}