using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class RunningStateConfig
{
    [SerializeField, Range(0, 10)] private float _speed;
    public float Speed => _speed;
}