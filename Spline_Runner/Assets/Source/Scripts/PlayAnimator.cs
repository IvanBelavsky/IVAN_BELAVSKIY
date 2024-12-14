using UnityEngine;

public class PlayAnimator : MonoBehaviour
{
    private readonly int Run = Animator.StringToHash(nameof(Run));
    private readonly int Finish = Animator.StringToHash(nameof(Finish));
    private readonly int Die = Animator.StringToHash(nameof(Die));
    
    [SerializeField] private Animator _animator;

    public void SetRun()
    {
        _animator.SetTrigger(Run);
    }

    public void SetFinish()
    {
        _animator.SetTrigger(Finish);
    }

    public void SetDie()
    {
        _animator.SetTrigger(Die);
    }
}