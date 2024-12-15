public class FallingState : AirBornState
{
    private GroundChecker _groundChecker;
    
    public FallingState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
        _groundChecker = character.GroundChecker;
    }

    public override void Enter()
    {
        base.Enter();
        
        CharacterView.StartFallingState();
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StopFallingState();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches)
        {
            Data.YVelocity = 0;
            
            if(IsHorizontaInputZero)
                StateSwicher.SwichState<IdleState>();
            else
                StateSwicher.SwichState<RunningState>();
        }
    }
}