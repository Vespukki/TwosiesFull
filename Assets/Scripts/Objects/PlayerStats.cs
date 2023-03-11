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

    [SerializeField] private float _overSpeedDeccel;
    public float OverSpeedDecceleration => _overSpeedDeccel;
    #endregion

    #region air
    [Header("Air")]

    [SerializeField] private float _airSpeedMultiplier;
    public float AirSpeedMultiplier => _airSpeedMultiplier;

    [SerializeField] private float _airAccelerationMultiplier;
    public float AirAccelerationMultiplier => _airAccelerationMultiplier;

    [SerializeField] private float _airDeccelerationMultiplier;
    public float AirDeccelerationMultiplier => _airDeccelerationMultiplier;

    [SerializeField] private float _airOverSpeedDeccelMultiplier;
    public float AirOverSpeedDeccelerationMultiplier => _airOverSpeedDeccelMultiplier;

    #endregion

    #region Jump
    [Header("Jump")]

    [SerializeField] private float _jumpHeight;
    public float JumpHeight => _jumpHeight;

    [SerializeField] private float _jumpTime;
    public float JumpTime => _jumpTime;

    [SerializeField] private float _jumpForgivenessTime;
    public float JumpForgivenessTime => _jumpForgivenessTime;

    #endregion
}
