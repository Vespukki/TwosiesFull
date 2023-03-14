using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Twosies.Player.Movement
{
public interface IJumpModifier
{
    public Vector2 JumpModifierVelocity { get; }
}
}