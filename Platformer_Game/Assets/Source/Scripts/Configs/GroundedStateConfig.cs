using System;
using UnityEngine;

[Serializable]
public class GroundedStateConfig
{
    [SerializeField] private RunningStateConfig _runningStateConfig;
    [SerializeField] private WalkingStateConfig _walkingStateConfig;

    public RunningStateConfig RunningStateConfig => _runningStateConfig;
    public WalkingStateConfig WalkingStateConfig => _walkingStateConfig;
}