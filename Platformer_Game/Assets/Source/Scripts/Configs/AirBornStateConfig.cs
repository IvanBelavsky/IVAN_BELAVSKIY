using System;
using UnityEngine;

[Serializable]
public class AirBornStateConfig
{
    [SerializeField] private JumpingStateConfig _jumpingStateConfig;
    
    public JumpingStateConfig JumpingStateConfig => _jumpingStateConfig;
    public float Gravity => 2 * _jumpingStateConfig.MaxHeight /
                            (_jumpingStateConfig.TimeToReachHeight * _jumpingStateConfig.TimeToReachHeight);
}