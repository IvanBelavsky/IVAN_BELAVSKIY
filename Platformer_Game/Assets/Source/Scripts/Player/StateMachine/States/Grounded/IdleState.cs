public class IdleState : GroundedState
{
    public IdleState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartIdleState();
        Data.Speed = 0;
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontaInputZero)
            return;

        StateSwicher.SwichState<RunningState>();
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StopIdleState();
    }
}