using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private const string IsIdle = "IsIdle";
    private const string IsRun = "IsRun";
    private const string IsJumping = "IsJumping";
    private const string IsFalling = "IsFalling";
    private const string IsMovement = "IsMovement";
    private const string IsAirBorn = "IsAirBorn";
    private const string IsGrounded = "IsGrounded";
    private const string IsWalk = "IsWalk";

    private Animator _animator;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartIdleState() => _animator.SetBool(IsIdle, true);
    public void StopIdleState() => _animator.SetBool(IsIdle, false);
    public void StartRunningState() => _animator.SetBool(IsRun, true);
    public void StopRunningState() => _animator.SetBool(IsRun, false);
    public void StartJumpingState() => _animator.SetBool(IsJumping, true);
    public void StopJumpingState() => _animator.SetBool(IsJumping, false);
    public void StartFallingState() => _animator.SetBool(IsFalling, true);
    public void StopFallingState() => _animator.SetBool(IsFalling, false);
    public void StartMovementState() => _animator.SetBool(IsMovement, true);
    public void StopMovementState() => _animator.SetBool(IsMovement, false);
    public void StartGraondedState() => _animator.SetBool(IsGrounded, true);
    public void StopGraondedState() => _animator.SetBool(IsGrounded, false);
    public void StartAirBornState() => _animator.SetBool(IsAirBorn, true);
    public void StoptAirBornState() => _animator.SetBool(IsAirBorn, false);
    public void StartWalkState() => _animator.SetBool(IsWalk, true);
    public void StoptWalkState() => _animator.SetBool(IsWalk, false);
}