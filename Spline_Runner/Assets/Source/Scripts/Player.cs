using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnDied;
    
    [SerializeField] private SplineFollower _splineFollower;
    [SerializeField] private PlayAnimator _playAnimator;

    public void StartLevel()
    {
        _splineFollower.Eneble();
        _playAnimator.SetRun();
    }

    public void Finished()
    {
        _splineFollower.Disable();
        _playAnimator.SetFinish();
        OnFinish?.Invoke();
    }

    public void Died()
    {
        _splineFollower.Disable();
        _playAnimator.SetDie();
        OnDied?.Invoke();
    }
}