using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModifier
{
    Vector2 MovementModifierVelocity { get; }
}
