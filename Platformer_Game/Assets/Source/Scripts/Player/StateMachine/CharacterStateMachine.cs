using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterStateMachine : IStateSwicher
{
    private List<IState> _states = new List<IState>();
    private IState _currentState;

    public CharacterStateMachine(Character character)
    {
        StateData stateData = new StateData();

        _states = new List<IState>()
        {
            new IdleState(this, stateData, character),
            new RunningState(this, stateData, character),
            new JumpingState(this, stateData, character),
            new FallingState(this, stateData, character),
            new WalkingState(this, stateData, character)
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwichState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);

        if (state == null)
            throw new ArgumentOutOfRangeException(nameof(state));

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();
}