public class RunningState : GroundedState
{
    private readonly RunningStateConfig _runningStateConfig;

    public RunningState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character) => _runningStateConfig = character.CharacterConfig.GroundedStateConfig.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartRunningState();
        Data.Speed = _runningStateConfig.Speed;
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
        
        CharacterView.StopRunningState();
    }
}