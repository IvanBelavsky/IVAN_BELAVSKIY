using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private GroundChecker _groundChecker;
    
    public GroundedState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
        _groundChecker = character.GroundChecker;
    }

    public override void Enter()
    {
        base.Enter();
        
        CharacterView.StartGraondedState();

        PlayerInput.Movement.Jump.started += DoJump;
        PlayerInput.Movement.Walk.started+= DoWalk;
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StopGraondedState();
        
        PlayerInput.Movement.Jump.started -= DoJump;
        PlayerInput.Movement.Walk.started -= DoWalk;
    }

    public override void Update()
    {
        base.Update();
        
        if(_groundChecker.IsTouches == false)
            StateSwicher.SwichState<FallingState>();
    }
    
    private void DoJump(InputAction.CallbackContext obj)
    {
        StateSwicher.SwichState<JumpingState>();   
    }
    
    private void DoWalk(InputAction.CallbackContext obj)
    {
        StateSwicher.SwichState<WalkingState>();
    }
}