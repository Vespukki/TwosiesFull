using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    #region walking
    [Header("Walking")]

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private float _acceleration;
    public float Acceleration => _acceleration;

    [SerializeField] private float _jerk;
    public float Jerk => _jerk;

    [SerializeField] private float _decceleration;
    public float Decceleration => _decceleration;

    [SerializeField] private float _movementTargetTolerance;
    public float MovementTargetTolerance => _movementTargetTolerance;
    #endregion

    #region air
    [Header("Air")]

    [SerializeField] private float _airSpeed;
    public float AirSpeed => _airSpeed;

    [SerializeField] private float _airAcceleration;
    public float AirAcceleration => _airAcceleration;

    [SerializeField] private float _airDecceleration;
    public float AirDecceleration => _airDecceleration;

    [SerializeField] private float _jumpHeight;
    public float JumpHeight => _jumpHeight;

    [SerializeField] private float _jumpTime;
    public float JumpTime => _jumpTime;

    [SerializeField] private float _jumpForgivenessTime;
    public float JumpForgivenessTime => _jumpForgivenessTime;
    #endregion
}
