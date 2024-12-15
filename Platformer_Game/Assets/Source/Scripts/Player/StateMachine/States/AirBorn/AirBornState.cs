using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class AirBornState : MovementState
{
    private readonly AirBornStateConfig _airBornStateConfig;
    public AirBornState(IStateSwicher stateSwicher, StateData data, Character character) : base(stateSwicher, data,
        character)
    {
        _airBornStateConfig = character.CharacterConfig.AirBornState;
    }

    public override void Enter()
    {
        base.Enter();
        
        CharacterView.StartAirBornState();
        
        Data.Speed = _airBornStateConfig.JumpingStateConfig.Speed;
    }

    public override void Exit()
    {
        base.Exit();
        
        CharacterView.StoptAirBornState();
    }

    public override void Update()
    {
        base.Update();

        Data.YVelocity -= _airBornStateConfig.Gravity * Time.deltaTime;
    }
}