public class JumpingState : AirBornState
{
    private readonly JumpingStateConfig _jumpingStateConfig;
    
    public JumpingState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
        _jumpingStateConfig = character.CharacterConfig.AirBornState.JumpingStateConfig;
    }

    public override void Enter()
    {
        base.Enter();
        
        CharacterView.StartJumpingState();
        
        Data.YVelocity = _jumpingStateConfig.StartYVelocity;
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StopJumpingState();
    }

    public override void Update()
    {
        base.Update();
        
        if(Data.YVelocity <=0)
            StateSwicher.SwichState<FallingState>();
    }
}