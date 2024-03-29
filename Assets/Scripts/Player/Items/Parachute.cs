using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twosies.States.Player;

namespace Twosies.Player.Items
{
public class Parachute : ItemBase
{
    public Parachute(PlayerStateMachine player) : base(player)
    {
        itemName = "Parachute";
    }


    public override void Use(PlayerStateMachine player)
    {
        player.body.mass = .5f;
        Debug.Log("parachute");
    }

    public override void UnUse(PlayerStateMachine player)
    {
        player.body.mass = 1;
    }

}
}