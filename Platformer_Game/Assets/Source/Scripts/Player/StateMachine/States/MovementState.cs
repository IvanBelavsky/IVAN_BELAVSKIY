using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStateSwicher StateSwicher;
    protected readonly StateData Data;

    private readonly Character _character;

    private Quaternion TurnLeft = Quaternion.Euler(0, 180, 0);
    private Quaternion TurnRight = new Quaternion(0, 0, 0, 0);

    public MovementState(IStateSwicher stateSwicher, StateData data, Character character)
    {
        StateSwicher = stateSwicher;
        Data = data;
        _character = character;
    }

    protected PlayerInput PlayerInput => _character.PlayerInput;
    protected CharacterController CharacterController => _character.CharacterController;
    protected CharacterView CharacterView => _character.CharacterView;
    protected bool IsHorizontaInputZero => Data.XInput == 0;

    public virtual void Enter()
    {
        CharacterView.StartMovementState();
        
        Debug.Log(GetType());
    }

    public virtual void Exit()
    {
        CharacterView.StopMovementState();
    }

    public virtual void HandleInput()
    {
        Data.XInput = HorizontalInput();
        Data.XVelocity = Data.XInput * Data.Speed;
    }

    public virtual void Update()
    {
        Vector3 velocity = GetConvertedVelocity();

        CharacterController.Move(velocity * Time.deltaTime);
        _character.transform.rotation = GetRotationFromVelocity(velocity);
    }

    private Quaternion GetRotationFromVelocity(Vector3 velocity)
    {
        if (velocity.x < 0)
            return TurnLeft;

        if (velocity.x > 0)
            return TurnRight;

        return _character.transform.rotation;
    }

    private Vector3 GetConvertedVelocity()
    {
        return new Vector3(Data.XVelocity, Data.YVelocity, 0);
    }

    private float HorizontalInput()
    {
        return PlayerInput.Movement.Move.ReadValue<float>();
    }
}