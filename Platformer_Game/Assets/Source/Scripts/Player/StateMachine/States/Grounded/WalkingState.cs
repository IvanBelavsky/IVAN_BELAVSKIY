public class WalkingState : GroundedState
{
    private WalkingStateConfig _walkingStateConfig;
    
    public WalkingState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
        _walkingStateConfig = character.CharacterConfig.GroundedStateConfig.WalkingStateConfig;
    }

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _walkingStateConfig.Speed;
        
        CharacterView.StartWalkState();
    }

    public override void Update()
    {
        base.Update();
        
        if (IsHorizontaInputZero)
            StateSwicher.SwichState<IdleState>();
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StoptWalkState();
    }
}