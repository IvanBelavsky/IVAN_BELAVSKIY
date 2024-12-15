using System;
using UnityEngine;

[Serializable]
public class JumpingStateConfig
{
    [SerializeField, Range(0, 10)] private float _maxHeight;
    [SerializeField, Range(0, 10)] private float _timeToReachHeight;
    [SerializeField, Range(0, 10)] private float _speed;

    public float StartYVelocity => 2 * _maxHeight / _timeToReachHeight;
    public float MaxHeight => _maxHeight;
    public float TimeToReachHeight => _timeToReachHeight;
    public float Speed => _speed;
}